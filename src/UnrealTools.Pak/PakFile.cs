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
        private readonly TempCompress _compressor = new TempCompress();
        private readonly IAesProvider _decryptor = new TempAES();

        public Dictionary<string, PakEntry> AbsoluteIndex { get; private set; } = null!;
        public int FileCount => AbsoluteIndex.Count;
        private string MountPoint { get; set; } = null!;
        private string FileName { get; }
        private string FilePath { get; }
        private FileStream FileStream { get; }

        private PakFile(FileInfo file, FileStream fileStream, IVersionProvider versionProvider)
        {
            FileName = file.Name;
            FilePath = file.FullName;
            FileStream = fileStream;
            _versionProvider = versionProvider;
        }
        private PakFile(FileInfo file, FileStream fileStream) : this(file, fileStream, AutomaticVersionProvider.Instance) { }

        public Memory<byte> ReadEntry(PakEntry entry)
        {
            var buf = ReadStream(entry.Offset + entry.EntryHeaderSize, entry.Size);
            if (entry.IsEncrypted)
            {
                //var aes = new AesCryptoServiceProvider();
                return null;
            }
            if (entry.IsCompressed)
            {
                Memory<byte> decompressed = new byte[entry.UncompressedSize];
                _compressor.Method = entry.CompressionMethod;
                _compressor.Decompress(buf.Memory.Span, decompressed.Span, entry.CompressionBlocks.Select(x => x.AbsoluteTo(entry.EntryHeaderSize)));
                return decompressed;
            }
            else
                return buf;
        }
        public IMemoryOwner<byte> ReadEntryOwner(PakEntry entry)
        {
            var buf = ReadStreamOwner(entry.Offset + entry.EntryHeaderSize, entry.Size);
            if (entry.IsEncrypted)
            {
                //var aes = new AesCryptoServiceProvider();
                return null;
            }
            if (entry.IsCompressed)
            {
                var decompressed = PakMemoryPool.Shared.Rent((int)entry.UncompressedSize);
                _compressor.Method = entry.CompressionMethod;
                _compressor.Decompress(buf.Memory.Span, decompressed.Memory.Span, entry.CompressionBlocks.Select(x => x.AbsoluteTo(entry.EntryHeaderSize)));
                buf.Dispose();
                return decompressed;
            }
            else
                return buf;
        }
        public async ValueTask<Memory<byte>> ReadEntryAsync(PakEntry entry, CancellationToken cancellationToken = default)
        {
            var taskData = ReadStreamAsync(entry.Offset + entry.EntryHeaderSize, entry.Size, cancellationToken);
            var buf = taskData.IsCompletedSuccessfully ? taskData.Result : await taskData.ConfigureAwait(false);
            if (entry.IsEncrypted)
            {
                //var aes = new AesCryptoServiceProvider();
                return null;
            }
            if (entry.IsCompressed)
            {
                Memory<byte> decompressed = new byte[entry.UncompressedSize];
                _compressor.Method = entry.CompressionMethod;
                _compressor.Decompress(buf.Span, decompressed.Span, entry.CompressionBlocks.Select(x => x.AbsoluteTo(entry.EntryHeaderSize)));
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
        private async Task InitializeAsync(PakInfo info, CancellationToken cancellationToken = default)
        {
            var reader = await info.ReadIndexAsync(FileStream, cancellationToken).ConfigureAwait(false);
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

        public static PakFile? Open(FileInfo fileInfo) => Open(fileInfo, AutomaticVersionProvider.Instance);
        public static PakFile? Open(FileInfo fileInfo, IVersionProvider versionProvider)
        {
            if (fileInfo is null) throw new ArgumentNullException(nameof(fileInfo));
            if (!fileInfo.Exists) throw new FileNotFoundException();

            var fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize);
            if (ReadPakInfo(fileStream, out var info))
            {
                var file = new PakFile(fileInfo, fileStream, versionProvider);
                file.Initialize(info);
                return file;
            }
            else
            {
                fileStream.Dispose();
                throw new NotPakFileException();
            }
        }
        public static ValueTask<PakFile?> OpenAsync(FileInfo fileInfo, CancellationToken cancellationToken = default) => OpenAsync(fileInfo, AutomaticVersionProvider.Instance, cancellationToken);
        public async static ValueTask<PakFile?> OpenAsync(FileInfo fileInfo, IVersionProvider versionProvider, CancellationToken cancellationToken = default)
        {
            if (fileInfo is null) throw new ArgumentNullException(nameof(fileInfo));
            if (!fileInfo.Exists) throw new FileNotFoundException();

            var fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize);
            if (ReadPakInfo(fileStream, out var info))
            {
                var file = new PakFile(fileInfo, fileStream);
                await file.InitializeAsync(info, cancellationToken).ConfigureAwait(false);
                return file;
            }

            return null;
        }

        private static bool ReadPakInfo(FileStream fileStream, [NotNullWhen(true)] out PakInfo? info)
        {
            var values = Enum.GetValues(typeof(PakInfoSize)).Cast<int>().ToArray();
            var len = fileStream.Length;
            var maxSize = (int)Math.Min(values.Max(), len);
            using var data = PakMemoryPool.Shared.Rent(maxSize);
            fileStream.ReadWholeBuf(-maxSize, SeekOrigin.End, data.Memory.Span);
            foreach (var value in values)
            {
                var a = (int)Math.Min(value, len);
                var z = data.Memory[^a..];
                var pak = new PakInfo(z);
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
