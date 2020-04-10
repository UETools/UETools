using System;
using UETools.Assets.Enums;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets
{
    [UnrealAssetFile(".locmeta")]
    public sealed class LocMetaAsset : IUnrealDeserializable
    {
        static Guid LocMetaMagic = new Guid(0xA14CEE4F, 0x4868, 0x8355, 0x6C, 0x4C, 0x46, 0xBD, 0x70, 0xDA, 0x50, 0x7C);

        private bool ReadVersion(FArchive reader, out ELocMetaVersion version)
        {
            reader.Read(out Guid MagicNumber);
            if (MagicNumber != LocMetaMagic)
            {
                version = ELocMetaVersion.Initial;
                return false;
            }
            reader.ReadUnsafe(out version);

            reader.AssetVersion = (int)version;
            return true;
        }

        public void Deserialize(FArchive reader)
        {
            if (ReadVersion(reader, out var version))
            {
                reader.Read(out _nativeCulture);
                reader.Read(out _nativeLocRes);
            }
        }

        private FString _nativeCulture = null!;
        private FString _nativeLocRes = null!;
    }
}
