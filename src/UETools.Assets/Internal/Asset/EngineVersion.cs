using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Asset
{
    public class EngineVersion : IUnrealSerializable
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

        public FArchive Serialize(FArchive archive)
            => archive.Read(ref _major)
                      .Read(ref _minor)
                      .Read(ref _patch)
                      .Read(ref _changelist)
                      .Read(ref _branch);

        private ushort _major;
        private ushort _minor;
        private ushort _patch;
        private uint _changelist;
        private FString _branch = null!;
    }
}
