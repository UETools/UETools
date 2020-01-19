using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;
using UnrealTools.Pak.Enums;

namespace UnrealTools.Pak
{
    public sealed partial class PakInfo : IUnrealDeserializable
    {
        public bool IsUnrealPak => _magic == PakFile.Magic;
        public FArchive ReadIndex(Stream stream)
        {
            var memory = PakMemoryPool.Shared.Rent((int)_indexSize);
            stream.ReadWholeBuf(_indexOffset, memory.Memory.Span);
            return new FArchive(memory)
            {
                AssetVersion = (int)_version,
                AssetSubversion = GetPakSubversion(),
            };
        }
        public async ValueTask<FArchive> ReadIndexAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            var memory = PakMemoryPool.Shared.Rent((int)_indexSize);
            var val = stream.ReadWholeBufAsync(_indexOffset, memory.Memory, cancellationToken);
            if (!val.IsCompletedSuccessfully)
                await val.ConfigureAwait(false);

            return new FArchive(memory)
            {
                AssetVersion = (int)_version,
                AssetSubversion = GetPakSubversion(),
            };
        }

        private const int CompressionMethodNameLen = 32;

        // TODO: Add stuff based on the backwards incompatible changes to pak format
        private int GetPakSubversion() => _infoSize switch
        {
            PakInfoSize.Sizev8 => 1,
            _ => 0
        };

        private PakInfo(PakInfoSize infoSize) => _infoSize = infoSize;
        public PakInfo(Memory<byte> data) : this((PakInfoSize)data.Length) => Deserialize(new FArchive(data));

        public void Deserialize(FArchive reader)
        {
            reader.Read(out _encryptionIndexGuid);
            reader.Read(out _encryptedIndex);
            reader.Read(out _magic);
            if (!IsUnrealPak)
                return;

            reader.ReadUnsafe(out _version);
            reader.Read(out _indexOffset);
            reader.Read(out _indexSize);
            reader.Read(out _indexHash);
            if (_version < PakVersion.IndexEncryption)
                _encryptedIndex = 0;
            if (_version < PakVersion.EncryptionKeyGuid)
                _encryptionIndexGuid = default;

            if (_version < PakVersion.FNameBasedCompressionMethod)
            {
                _compressionMethods.Add("Zlib");
                _compressionMethods.Add("Gzip");
                _compressionMethods.Add("Oodle");
            }
            else
            {
                var remainingBytes = (int)(reader.Length() - reader.Tell());
                reader.Read(out Span<byte> shit, remainingBytes);
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
        }

        private uint _magic;
        private PakVersion _version;
        private long _indexOffset;
        private long _indexSize;
        private SHA1Hash _indexHash;
        private byte _encryptedIndex;
        private Guid _encryptionIndexGuid;
        private List<FName> _compressionMethods = new List<FName>();

        private PakInfoSize _infoSize;
    }
}