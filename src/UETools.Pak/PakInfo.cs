using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Pak.Enums;

namespace UETools.Pak
{
    internal sealed partial class PakInfo : IUnrealSerializable
    {
        public bool IsUnrealPak => _magic == PakFile.Magic;
        public bool IsEncrypted => _encryptedIndex != 0;
        public FArchive ReadIndex(Stream stream)
        {
            var memory = PakMemoryPool.Shared.Rent((int)_indexSize);
            stream.ReadWholeBuf(_indexOffset, memory.Memory.Span);

            if (IsEncrypted && _aesProvider != null)
                _aesProvider.Decrypt(memory.Memory);

            return new FArchive(memory)
            {
                AssetVersion = (int)_version,
                AssetSubversion = GetPakSubversion(),
            };
        }
        public async ValueTask<FArchive> ReadIndexAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            var memory = PakMemoryPool.Shared.Rent((int)_indexSize);
            await stream.ReadWholeBufAsync(_indexOffset, memory.Memory, cancellationToken).ConfigureAwait(false);

            if (IsEncrypted && _aesProvider != null)
                _aesProvider.Decrypt(memory.Memory);

            return new FArchive(memory)
            {
                AssetVersion = (int)_version,
                AssetSubversion = GetPakSubversion(),
            };
        }

        // TODO: Add stuff based on the backwards incompatible changes to pak format
        private int GetPakSubversion() => _infoSize switch
        {
            PakInfoSize.Sizev8 => 1,
            _ => 0
        };

        private PakInfo(PakInfoSize infoSize) => _infoSize = infoSize;
        internal PakInfo(Memory<byte> data, AesPakCryptoProvider? aesProvider) : this((PakInfoSize)data.Length)
        {
            _aesProvider = aesProvider;
            using var ar = new FArchive(data);
            Serialize(ar);
        }

        public FArchive Serialize(FArchive archive)
        {
            archive.Read(ref _encryptionIndexGuid)
                   .Read(ref _encryptedIndex)
                   .Read(ref _magic);

            if (!IsUnrealPak)
                return archive;

            archive.ReadUnsafe(ref _version)
                   .Read(ref _indexOffset)
                   .Read(ref _indexSize)
                   .Read(ref _indexHash);
            if (_version < PakVersion.IndexEncryption)
                _encryptedIndex = 0;
            if (_version < PakVersion.EncryptionKeyGuid)
                _encryptionIndexGuid = default;

            if (_version >= PakVersion.FrozenIndex)
                archive.Read(ref _indexIsFrozen);

            if (_version < PakVersion.FNameBasedCompressionMethod)
            {
                _compressionMethods.Add("Zlib");
                _compressionMethods.Add("Gzip");
                _compressionMethods.Add("Oodle");
            }
            else
            {
                var remainingBytes = (int)(archive.Length() - archive.Tell());
                Span<byte> shit = default;
                archive.Read(ref shit, remainingBytes);
                for (int Index = 0, start = 0; start < shit.Length; start = ++Index * CompressionMethodNameLen)
                {
                    var MethodString = shit.Slice(start, CompressionMethodNameLen);
                    if (MethodString[0] != 0)
                    {
                        var chars = MemoryMarshal.Cast<byte, char>(MethodString.Slice(0, MethodString.IndexOf((byte)0)));
                        _compressionMethods.Add(chars.ToString());
                    }
                }
            }

            return archive;
        }

        private uint _magic;
        private PakVersion _version;
        private long _indexOffset;
        private long _indexSize;
        private SHA1Hash _indexHash;
        private byte _encryptedIndex;
        private Guid _encryptionIndexGuid;
        private byte _indexIsFrozen;
        private List<FName> _compressionMethods = new List<FName>();

        private PakInfoSize _infoSize;
        private AesPakCryptoProvider? _aesProvider;
        private const int CompressionMethodNameLen = 32;
    }
}