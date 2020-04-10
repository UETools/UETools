﻿using System;
using UETools.Assets.Enums;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Registry
{
    public sealed class AssetPackageData : IUnrealDeserializable
    {
        public void Deserialize(FArchive reader)
        {
            var ver = (EAssetRegistryVersion)reader.AssetVersion;
            reader.Read(out _diskSize);
            reader.Read(out _packageGuid);
            if (ver >= EAssetRegistryVersion.AddedCookedMD5Hash || ver < EAssetRegistryVersion.RemovedMD5Hash)
                reader.Read(out _hash);
        }

        private long _diskSize;
        private Guid _packageGuid;
        private MD5Hash _hash;
    }
}
