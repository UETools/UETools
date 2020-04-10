using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Asset
{
    public class EngineVersion : IUnrealDeserializable
    {
        public EngineVersion() { }
        public EngineVersion(ushort major, ushort minor, ushort patch, uint changelist, string branch)
        {
            _major = major;
            _minor = minor;
            _patch = patch;
            _changelist = changelist;
            _branch = branch;
        }

        public void Deserialize(FArchive reader)
        {
            reader.Read(out _major);
            reader.Read(out _minor);
            reader.Read(out _patch);
            reader.Read(out _changelist);
            reader.Read(out _branch);
        }

        private ushort _major;
        private ushort _minor;
        private ushort _patch;
        private uint _changelist;
        private FString _branch = null!;
    }
}
