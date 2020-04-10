using System;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    public partial class CustomVersionContainer
    {
        public class CustomVersion : IUnrealDeserializable
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
            public void Deserialize(FArchive reader)
            {
                reader.Read(out _key);
                reader.Read(out _version);
            }

            private Guid _key;
            private int _version;
        }
        class GuidCustomVersion : IUnrealDeserializable
        {
            public Guid Key => _key;
            public int Version => _version;
            public FString FriendlyName => _friendlyName;

            public void Deserialize(FArchive reader)
            {
                reader.Read(out _key);
                reader.Read(out _version);
                reader.Read(out _friendlyName);
            }

            public static implicit operator CustomVersion(GuidCustomVersion ver) => new CustomVersion(ver.Key, ver.Version, ver.FriendlyName);

            private Guid _key;
            private int _version;
            private FString _friendlyName = null!;
        }
        class EnumCustomVersion : IUnrealDeserializable
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
            public void Deserialize(FArchive reader)
            {
                reader.Read(out _tag);
                reader.Read(out _version);
            }

            public static implicit operator CustomVersion(EnumCustomVersion ver) => new CustomVersion(ver.TagGuid, ver.Version, "EnumTag" + ver.Tag);

            private int _tag;
            private int _version;
        }
    }
}
