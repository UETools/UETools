using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;
using UnrealTools.Pak.Enums;
using UnrealTools.Pak.Interfaces;

namespace UnrealTools.Pak
{
    [DebuggerDisplay("{FileName}, {AbsoluteIndex.Count} files", Name = "PakFile {FileName}")]
    public sealed class PakFile : IDisposable, IAsyncDisposable
    {
        public const uint Magic = 0x5A6F12E1;
        public const int BufferSize = 32 * 1024;
        private readonly IVersionProvider _versionProvider;
        private readonly AesPakCryptoProvider? _aesProvider;
        private readonly TempCompress _compressor = new TempCompress();

        public Dictionary<string, PakEntry> AbsoluteIndex { get; private set; } = null!;
        public int FileCount => AbsoluteIndex.Count;
        private string MountPoint { get; set; } = null!;
        private string FileName { get; }
        private string FilePath { get; }
        private FileStream FileStream { get; }

        private PakFile(FileInfo file, FileStream fileStream, IVersionProvider? versionProvider = null, AesPakCryptoProvider? aesProvider = null)
        {
            FileName = file.Name;
            FilePath = file.FullName;
            FileStream = fileStream;
            _aesProvider = aesProvider;
            _versionProvider = versionProvider ?? AutomaticVersionProvider.Instance;
        }

        public IMemoryOwner<byte> ReadEntry(PakEntry entry)
        {
            IMemoryOwner<byte>? buf = ReadStream(entry.Offset + entry.EntryHeaderSize, entry.Size);
            if (entry.IsEncrypted)
            {
                if (_aesProvider is null)
                    throw new PakEncryptedException("Pak file contains encrypted entries. AES encryption key is necessary for reading this asset.");

                _aesProvider.Decrypt(buf.Memory);
            }
            if (entry.IsCompressed)
            {
                var decompressed = PakMemoryPool.Shared.Rent((int)entry.UncompressedSize);
                _compressor.Method = entry.CompressionMethod;
                _compressor.Decompress(ref buf, decompressed.Memory.Span, entry.CompressionBlocks.Select(x => x.AbsoluteTo(entry.EntryHeaderSize)));
                return decompressed;
            }
            else
                return buf;
        }
        public async ValueTask<IMemoryOwner<byte>> ReadEntryAsync(PakEntry entry, CancellationToken cancellationToken = default)
        {
            var taskData = ReadStreamAsync(entry.Offset + entry.EntryHeaderSize, entry.Size, cancellationToken);
            IMemoryOwner<byte>? buf = taskData.IsCompletedSuccessfully ? taskData.Result : await taskData.ConfigureAwait(false);
            if (entry.IsEncrypted)
            {
                if (_aesProvider is null)
                    throw new PakEncryptedException("Pak file contains encrypted entries. AES encryption key is necessary for reading this asset.");

                _aesProvider.Decrypt(buf.Memory);
            }
            if (entry.IsCompressed)
            {
                var decompressed = PakMemoryPool.Shared.Rent((int)entry.UncompressedSize);
                _compressor.Method = entry.CompressionMethod;
                _compressor.Decompress(ref buf, decompressed.Memory.Span, entry.CompressionBlocks.Select(x => x.AbsoluteTo(entry.EntryHeaderSize)));
                return decompressed;
            }
            else
                return buf;
        }

        private void Initialize(PakInfo info)
        {
            var reader = info.ReadIndex(FileStream);
            reader.Read(out FString mountPoint);
            MountPoint = mountPoint.ToString().Replace("../../../", null);
            reader.Read(out int NumEntries);
            AbsoluteIndex = new Dictionary<string, PakEntry>(NumEntries);
            for (var i = 0; i < NumEntries; i++)
            {
                reader.Read(out FString fileName);
                var filePath = Path.Combine(MountPoint, fileName.ToString());
                var entry = new PakEntry(this);
                entry.Deserialize(reader);
                AbsoluteIndex.Add(filePath, entry);
            }
        }
        private async ValueTask InitializeAsync(PakInfo info, CancellationToken cancellationToken = default)
        {
            var reader = await info.ReadIndexAsync(FileStream, cancellationToken: cancellationToken).ConfigureAwait(false);
            reader.Read(out FString mountPoint);
            MountPoint = mountPoint.ToString().Replace("../../../", null);
            reader.Read(out int NumEntries);
            AbsoluteIndex = new Dictionary<string, PakEntry>(NumEntries);
            for (var i = 0; i < NumEntries; i++)
            {
                reader.Read(out FString fileName);
                var filePath = Path.Combine(MountPoint, fileName.ToString());
                var entry = new PakEntry(this);
                entry.Deserialize(reader);
                AbsoluteIndex.Add(filePath, entry);
            }
        }

        public static PakFile? Open(FileInfo fileInfo, IVersionProvider? versionProvider = null, IAesKeyProvider? keyProvider = null)
        {
            if (fileInfo is null) throw new ArgumentNullException(nameof(fileInfo));
            if (!fileInfo.Exists) throw new FileNotFoundException();
            var aesProvider = keyProvider is null ? null : new AesPakCryptoProvider(keyProvider);

            var fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize);
            if (ReadPakInfo(fileStream, aesProvider, out var info))
            {
                var file = new PakFile(fileInfo, fileStream, versionProvider, aesProvider);
                file.Initialize(info);
                return file;
            }
            else
            {
                fileStream.Dispose();
                throw new NotPakFileException();
            }
        }
        public async static Task<PakFile?> OpenAsync(FileInfo fileInfo, IVersionProvider? versionProvider = null, IAesKeyProvider? keyProvider = null, CancellationToken cancellationToken = default)
        {
            if (fileInfo is null) throw new ArgumentNullException(nameof(fileInfo));
            if (!fileInfo.Exists) throw new FileNotFoundException();
            var aesProvider = keyProvider is null ? null : new AesPakCryptoProvider(keyProvider);

            var fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize);
            if (ReadPakInfo(fileStream, aesProvider, out var info))
            {
                var file = new PakFile(fileInfo, fileStream, versionProvider, aesProvider);
                var task = file.InitializeAsync(info, cancellationToken);
                if(!task.IsCompletedSuccessfully)
                    await task.ConfigureAwait(false);

                return file;
            }

            return null;
        }

        private static bool ReadPakInfo(FileStream fileStream, AesPakCryptoProvider? _aesProvider, [NotNullWhen(true)] out PakInfo? info)
        {
            var values = Enum.GetValues(typeof(PakInfoSize)).Cast<int>();
            var len = fileStream.Length;
            var maxSize = (int)Math.Min(values.Max(), len);
            using var data = PakMemoryPool.Shared.Rent(maxSize);
            fileStream.ReadWholeBuf(-maxSize, SeekOrigin.End, data.Memory.Span);
            foreach (var value in values)
            {
                var a = (int)Math.Min(value, len);
                var z = data.Memory[^a..];
                var pak = new PakInfo(z, _aesProvider);
                if (pak.IsUnrealPak)
                {
                    info = pak;
                    return true;
                }
            }
            info = null;
            return false;
        }

        private IMemoryOwner<byte> ReadStream(long offset, long size)
        {
            var data = PakMemoryPool.Shared.Rent((int)size);
            FileStream.ReadWholeBuf(offset, data.Memory.Span);
            return data;
        }
        private async ValueTask<IMemoryOwner<byte>> ReadStreamAsync(long offset, long size, CancellationToken cancellationToken)
        {
            var data = PakMemoryPool.Shared.Rent((int)size);
            var result = FileStream.ReadWholeBufAsync(offset, data.Memory, cancellationToken);
            if (!result.IsCompletedSuccessfully)
                await result.ConfigureAwait(false);

            return data;
        }

        public void Dispose() => FileStream.Dispose();
        public ValueTask DisposeAsync() => FileStream.DisposeAsync();
    }
}
