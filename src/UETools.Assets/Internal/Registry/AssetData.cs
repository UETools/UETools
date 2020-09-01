using System.Collections.Generic;
using UETools.Assets.Interfaces;
using UETools.Core;
using UETools.Core.Enums;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Registry
{
    public class AssetData : IAssetData, IUnrealSerializable
    {
        public FName ObjectPath => _objectPath;
        public FName PackagePath => _packagePath;
        public FName AssetClass => _assetClass;
        public FName PackageName => _packageName;
        public FName AssetName => _assetName;
        public Dictionary<FName, FString> TagsAndValues => _tagsAndValues;
        public List<int> ChunkIDs => _chunkIDs;
        public EPackageFlags PackageFlags => _packageFlags;

        public FArchive Serialize(FArchive archive)
        {
            archive.Read(ref _objectPath)
                   .Read(ref _packagePath)
                   .Read(ref _assetClass)
                   .Read(ref _packageName)
                   .Read(ref _assetName)
                   .Read(ref _tagsAndValues);

            if (archive.Version >= UE4Version.VER_UE4_CHANGED_CHUNKID_TO_BE_AN_ARRAY_OF_CHUNKIDS)
                archive.Read(ref _chunkIDs);
            else if (archive.Version >= UE4Version.VER_UE4_ADDED_CHUNKID_TO_ASSETDATA_AND_UPACKAGE)
                archive.Read(ref _chunkIDs, 1);
            else
                _chunkIDs = new List<int>();

            if (archive.Version >= UE4Version.VER_UE4_COOKED_ASSETS_IN_EDITOR_SUPPORT)
                archive.ReadUnsafe(ref _packageFlags);

            return archive;
        }

        private FName _objectPath = null!;
        private FName _packagePath = null!;
        private FName _assetClass = null!;
        private FName _packageName = null!;
        private FName _assetName = null!;
        private Dictionary<FName, FString> _tagsAndValues = null!;

        private List<int> _chunkIDs = null!;
        private EPackageFlags _packageFlags;
    }
}
