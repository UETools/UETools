using UETools.Core.Enums;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    internal class NameEntrySerialized : IUnrealSerializable
    {
        public FString Name { get => _name; set => _name = value; }

        public FArchive Serialize(FArchive reader)
        {
            reader.Read(ref _name);
            if (reader.Version >= UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)
            {
                reader.Read(ref _nonCasePreservingHash)
                      .Read(ref _casePreservingHash);
            }
            return reader;
        }

        private FString _name = null!;
        private ushort _nonCasePreservingHash;
        private ushort _casePreservingHash;
    }
}
