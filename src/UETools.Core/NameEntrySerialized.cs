using UETools.Core.Enums;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    internal class NameEntrySerialized : IUnrealDeserializable
    {
        public FString Name { get => _name; set => _name = value; }
        public ushort NonCasePreservingHash { get => _nonCasePreservingHash; set => _nonCasePreservingHash = value; }
        public ushort CasePreservingHash { get => _casePreservingHash; set => _casePreservingHash = value; }

        public void Deserialize(FArchive reader)
        {
            reader.Read(out _name);
            if (reader.Version >= UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)
            {
                reader.Read(out _nonCasePreservingHash);
                reader.Read(out _casePreservingHash);
            }
        }

        private FString _name = null!;
        private ushort _nonCasePreservingHash;
        private ushort _casePreservingHash;
    }
}
