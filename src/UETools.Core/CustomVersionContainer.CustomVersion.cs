using System;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    public partial class CustomVersionContainer
    {
        public class CustomVersion : IUnrealSerializable
        {
            public Guid Key => _key;
            public int Version => _version;
            public int ReferenceCount => 1;
            public FString FriendlyName { get; } = null!;

            public CustomVersion()
            {
            }
            public CustomVersion(Guid key, int version, string friendlyName)
            {
                _key = key;
                _version = version;
                FriendlyName = friendlyName;
            }
            public FArchive Serialize(FArchive archive)
                => archive.Read(ref _key)
                          .Read(ref _version);

            private Guid _key;
            private int _version;
        }
    }
}
