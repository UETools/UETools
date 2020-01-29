using System.Collections.Generic;
using UnrealTools.Assets.Interfaces;
using UnrealTools.Core;
using UnrealTools.Core.Enums;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Assets.Internal.Registry
{
    public class AssetData : IAssetData, IUnrealDeserializable
    {
        public FName ObjectPath => _objectPath;
        public FName PackagePath => _packagePath;
        public FName AssetClass => _assetClass;
        public FName PackageName => _packageName;
        public FName AssetName => _assetName;
        public Dictionary<FName, FString> TagsAndValues => _tagsAndValues;
        public List<int> ChunkIDs => _chunkIDs;
        public EPackageFlags PackageFlags => _packageFlags;

        public void Deserialize(FArchive reader)
        {
            reader.Read(out _objectPath);
            reader.Read(out _packagePath);
            reader.Read(out _assetClass);

            reader.Read(out _packageName);
            reader.Read(out _assetName);

            reader.Read(out _tagsAndValues);

            if (reader.Version >= UE4Version.VER_UE4_CHANGED_CHUNKID_TO_BE_AN_ARRAY_OF_CHUNKIDS)
                reader.Read(out _chunkIDs);
            else if (reader.Version >= UE4Version.VER_UE4_ADDED_CHUNKID_TO_ASSETDATA_AND_UPACKAGE)
                reader.Read(out _chunkIDs, 1);
            else
                _chunkIDs = new List<int>();

            if (reader.Version >= UE4Version.VER_UE4_COOKED_ASSETS_IN_EDITOR_SUPPORT)
                reader.ReadUnsafe(out _packageFlags);
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
