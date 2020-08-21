using System;
using System.Collections.Generic;
using UETools.Assets.Enums;
using UETools.Assets.Interfaces;
using UETools.Assets.Internal.Registry;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets
{
    [UnrealAssetFile(FileName, false)]
    public sealed class AssetRegistry : IUnrealSerializable
    {
        private static Guid AssetRegistryVersionGuid = new Guid(0x717F9EE7, 0x493A, 0xE9B0, 0x32, 0x91, 0xB3, 0x88, 0x07, 0x81, 0x38, 0x1B);

        public const string FileName = "AssetRegistry.bin";

        public List<IAssetData> Assets => _assets; 
        public Dictionary<FName, AssetPackageData> PackageData => _packageData;
        public Dictionary<AssetIdentifier, AssetDependencies> Dependencies => _assetDependencies;

        /// <inheritdoc />
        /// <exception cref="UnrealException">Thrown when <see cref="FArchive.Version"/> is not set.</exception>
        public FArchive Serialize(FArchive reader)
        {
            if (reader.Version == 0)
                throw new UnrealException("FArchive.Version is uninitialized, needs to be set for cooked assets.");

            ReadVersion(reader);

            // AssetData format was changed from FStrings to FNames
            if (_version >= EAssetRegistryVersion.ChangedAssetData)
            {
                long stringTableOffset = 0;
                reader.Read(ref stringTableOffset);
                var currentOffset = reader.Tell();
                reader.Seek(stringTableOffset);
                NameTable? names = default;
                reader.Read(ref names);
                reader.Seek(currentOffset);
                List<AssetData>? assets = default;
                reader.Read(ref assets);
                _assets = new List<IAssetData>(assets);
            }
            else
            {
                List<AssetDataOldFormat>? assets = default;
                reader.Read(ref assets);
                _assets = new List<IAssetData>(assets);
            }

            // TODO: Fixup the dependencies indexes into the references to actual dependency
            reader.Read(ref _assetDependencies)
                  .Read(ref _packageData);
            return reader;
        }

        private void ReadVersion(FArchive reader)
        {
            Guid guid = default;
            reader.Read(ref guid);
            if (guid == AssetRegistryVersionGuid)
            {
                reader.ReadUnsafe(ref _version);
            }
            else
            {
                _version = EAssetRegistryVersion.PreVersioning;
                reader.Seek(0);
            }
            reader.AssetVersion = (int)_version;
        }

        private EAssetRegistryVersion _version;
        private List<IAssetData> _assets = null!;
        private Dictionary<AssetIdentifier, AssetDependencies> _assetDependencies = null!;
        private Dictionary<FName, AssetPackageData> _packageData = null!;
    }
}
