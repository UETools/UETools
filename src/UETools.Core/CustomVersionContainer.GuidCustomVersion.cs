using System;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    public partial class CustomVersionContainer
    {
        class GuidCustomVersion : IUnrealSerializable
        {
            public Guid Key => _key;
            public int Version => _version;
            public FString FriendlyName => _friendlyName;

            public FArchive Serialize(FArchive archive)
                => archive.Read(ref _key)
                          .Read(ref _version)
                          .Read(ref _friendlyName);

            public static implicit operator CustomVersion(GuidCustomVersion ver) => new CustomVersion(ver.Key, ver.Version, ver.FriendlyName);

            private Guid _key;
            private int _version;
            private FString _friendlyName = null!;
        }
    }
}
