using System;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    public partial class CustomVersionContainer
    {
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
            public FArchive Serialize(FArchive archive)
                => archive.Read(ref _tag)
                          .Read(ref _version);

            public static implicit operator CustomVersion(EnumCustomVersion ver) => new CustomVersion(ver.TagGuid, ver.Version, "EnumTag" + ver.Tag);

            private int _tag;
            private int _version;
        }
    }
}
