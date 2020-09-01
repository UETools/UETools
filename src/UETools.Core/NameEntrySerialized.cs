using UETools.Core.Enums;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    internal class NameEntrySerialized : IUnrealSerializable
    {
        public FString Name { get => _name; set => _name = value; }

        public FArchive Serialize(FArchive archive)
        {
            archive.Read(ref _name);
            if (archive.Version >= UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)
            {
                archive.Read(ref _nonCasePreservingHash)
                       .Read(ref _casePreservingHash);
            }
            return archive;
        }

        private FString _name = null!;
        private ushort _nonCasePreservingHash;
        private ushort _casePreservingHash;
    }
}
