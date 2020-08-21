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
            public FArchive Serialize(FArchive reader)
                => reader.Read(ref _key)
                         .Read(ref _version);

            private Guid _key;
            private int _version;
        }
        class GuidCustomVersion : IUnrealSerializable
        {
            public Guid Key => _key;
            public int Version => _version;
            public FString FriendlyName => _friendlyName;

            public FArchive Serialize(FArchive reader) 
                => reader.Read(ref _key)
                         .Read(ref _version)
                         .Read(ref _friendlyName);

            public static implicit operator CustomVersion(GuidCustomVersion ver) => new CustomVersion(ver.Key, ver.Version, ver.FriendlyName);

            private Guid _key;
            private int _version;
            private FString _friendlyName = null!;
        }
        class EnumCustomVersion : IUnrealSerializable
        {
            private Guid TagGuid
            {
                get
                {
                    Span<byte> bytes = stackalloc byte[16];
                    BitConverter.GetBytes(Tag).CopyTo(bytes.Slice(12));
                    return new Guid(bytes);
                }
            }
            public int Tag => _tag;
            public int Version => _version;
            public FArchive Serialize(FArchive reader) 
                => reader.Read(ref _tag)
                         .Read(ref _version);

            public static implicit operator CustomVersion(EnumCustomVersion ver) => new CustomVersion(ver.TagGuid, ver.Version, "EnumTag" + ver.Tag);

            private int _tag;
            private int _version;
        }
    }
}
