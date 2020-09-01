using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Pak.Enums;
using UETools.Pak.Interfaces;

namespace UETools.Pak
{
    /// <summary>
    /// Virtual file system entry in pak file.
    /// </summary>
    public class PakEntry : IUnrealSerializable, IEntry
    {
        private PakFile Owner { get; }
        internal PakEntry LinkedEntry { get; set; } = null!;
        internal long EntryHeaderSize { get; private set; }

        public long Size => _size;
        public long Offset => _offset;
        public bool IsEncrypted => _flags == EPakEntryFlags.Encrypted;
        public bool IsCompressed => _compressionIndex != 0;

        public int CompressionMethod => _compressionIndex;
        public long UncompressedSize => _uncompressedSize;
        private uint CompressionBlockSize => _compressionBlockSize;

        public List<PakCompressedBlock> CompressionBlocks => _compressionBlocks;

        public PakEntry(PakFile pakFile) => Owner = pakFile;

        public FArchive Serialize(FArchive archive)
        {
            var start = archive.Tell();
            var pakVersion = (PakVersion)archive.AssetVersion;
            archive.Read(ref _offset)
                   .Read(ref _size)
                   .Read(ref _uncompressedSize);
            if (pakVersion >= PakVersion.FNameBasedCompressionMethod)
            {
                // backwards incompatible 4.22
                if (archive.AssetSubversion == 1)
                {
                    var index = (byte)_compressionIndex;
                    archive.Read(ref index);
                    _compressionIndex = index;
                }
                else
                    archive.Read(ref _compressionIndex);
            }
            else
            {
                ECompressionFlags legacyFlags = default;
                archive.ReadUnsafe(ref legacyFlags);
                if (legacyFlags == ECompressionFlags.COMPRESS_None)
                    _compressionIndex = 0;
                else if ((legacyFlags & ECompressionFlags.COMPRESS_ZLIB) != 0)
                    _compressionIndex = 1;
                else if ((legacyFlags & ECompressionFlags.COMPRESS_GZIP) != 0)
                    _compressionIndex = 2;
                else if ((legacyFlags & ECompressionFlags.COMPRESS_Custom) != 0)
                    _compressionIndex = 3;
                else
                    throw new NotImplementedException();
            }

            if (pakVersion < PakVersion.NoTimestamps)
            {
                long timestamp = 0;
                archive.Read(ref timestamp); // FDateTime Timestamp
            }

            archive.Read(ref _hash);
            if (pakVersion >= PakVersion.CompressionEncryption)
            {
                if (_compressionIndex != 0)
                    archive.Read(ref _compressionBlocks!);

                archive.ReadUnsafe(ref _flags)
                       .Read(ref _compressionBlockSize);
            }
            EntryHeaderSize = archive.Tell() - start;

            return archive;
        }

        public FArchive Read() => new FArchive(Owner.ReadEntry(this));
        public IMemoryOwner<byte> ReadBytes() => Owner.ReadEntry(this);
        public async ValueTask<FArchive> ReadAsync(CancellationToken cancellationToken = default) => new FArchive(await Owner.ReadEntryAsync(this, cancellationToken).ConfigureAwait(false));

        internal IMemoryOwner<byte> Read(Stream source)
        {
            var mem = PakMemoryPool.Shared.Rent((int)TotalSize);
            Read(source, mem.Memory);
            return mem;
        }
        internal async ValueTask<IMemoryOwner<byte>> ReadAsync(Stream source, CancellationToken cancellationToken = default)
        {
            var mem = PakMemoryPool.Shared.Rent((int)TotalSize);
            await ReadAsync(source, mem.Memory, cancellationToken).ConfigureAwait(false);
            return mem;
        }

        private void Read(Stream source, Memory<byte> destination)
        {
            var size = (int)_size;
            source.ReadWholeBuf(Offset + EntryHeaderSize, destination.Span.Slice(0, size));
            LinkedEntry?.Read(source, destination.Slice(size));
        }
        private async ValueTask ReadAsync(Stream source, Memory<byte> destination, CancellationToken cancellationToken = default)
        {
            var size = (int)_size;
            await source.ReadWholeBufAsync(Offset + EntryHeaderSize, destination.Slice(0, size), cancellationToken).ConfigureAwait(false);
            if (LinkedEntry is null)
                return;

            await LinkedEntry.ReadAsync(source, destination.Slice(size), cancellationToken).ConfigureAwait(false);
        }

        internal long TotalSize => LinkedEntry is null ? _size : _size + LinkedEntry.TotalSize;
        internal long TotalUncompressedSize => LinkedEntry is null ? _uncompressedSize : _uncompressedSize + LinkedEntry.TotalUncompressedSize;
        internal bool IsAnyCompressed => LinkedEntry is null ? IsCompressed : IsCompressed || LinkedEntry.IsAnyCompressed;
        internal bool IsAnyEncrypted => LinkedEntry is null ? IsEncrypted : IsEncrypted || LinkedEntry.IsAnyEncrypted;

        private long _offset;
        private long _size;
        private long _uncompressedSize;

        private int _compressionIndex;

        private List<PakCompressedBlock> _compressionBlocks = new List<PakCompressedBlock>();
        private uint _compressionBlockSize;
        private EPakEntryFlags _flags;
        private SHA1Hash _hash;
    }
}
