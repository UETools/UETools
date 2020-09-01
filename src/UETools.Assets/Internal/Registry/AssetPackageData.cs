using System;
using UETools.Assets.Enums;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Registry
{
    public sealed class AssetPackageData : IUnrealSerializable
    {
        public FArchive Serialize(FArchive archive)
        {
            var ver = (EAssetRegistryVersion)archive.AssetVersion;
            archive.Read(ref _diskSize)
                   .Read(ref _packageGuid);
            if (ver >= EAssetRegistryVersion.AddedCookedMD5Hash || ver < EAssetRegistryVersion.RemovedMD5Hash)
                archive.Read(ref _hash);

            return archive;
        }

        private long _diskSize;
        private Guid _packageGuid;
        private MD5Hash _hash;
    }
}
