using System.Collections.Generic;
using System.Linq;
using UnrealTools.Assets.Interfaces;
using UnrealTools.Assets.Interfaces.Generic;
using UnrealTools.Core;
using UnrealTools.Core.Enums;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Assets.Internal.Registry
{
    public class AssetDataOldFormat : IAssetData, IAssetData<FString>, IUnrealDeserializable
    {
        public FName ObjectPath { get => _objectPath; set => _objectPath = value.Name; }
        public FName PackagePath { get => _packagePath; set => _packagePath = value.Name; }
        public FName AssetClass { get => _assetClass; set => _assetClass = value.Name; }
        public FName PackageName { get => _packageName; set => _packageName = value.Name; }
        public FName AssetName { get => _assetName; set => _assetName = value.Name; }
        public Dictionary<FName, FString> TagsAndValues { get => _tagsAndValues.ToDictionary(kv => new FName(kv.Key), kv => kv.Value); set => _tagsAndValues = value.ToDictionary(kv => kv.Key.Name, kv => kv.Value); }

        public FString GroupNames { get => _groupNames; set => _groupNames = value; }

        FString IAssetData<FString>.ObjectPath => _objectPath;
        FString IAssetData<FString>.PackagePath => _packagePath;
        FString IAssetData<FString>.AssetClass => _assetClass;
        FString IAssetData<FString>.PackageName => _packageName;
        FString IAssetData<FString>.AssetName => _assetName;
        Dictionary<FString, FString> IAssetData<FString>.TagsAndValues => _tagsAndValues;

        public List<int> ChunkIDs => _chunkIDs;
        public EPackageFlags PackageFlags => _packageFlags;

        public void Deserialize(FArchive reader)
        {
            reader.Read(out _objectPath);
            reader.Read(out _packagePath);
            reader.Read(out _assetClass);
            reader.Read(out _groupNames);

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

        private FString _objectPath = null!;
        private FString _packagePath = null!;
        private FString _assetClass = null!;
        private FString _packageName = null!;
        private FString _assetName = null!;
        private Dictionary<FString, FString> _tagsAndValues = null!;

        private List<int> _chunkIDs = null!;
        private EPackageFlags _packageFlags;

        private FString _groupNames = null!;
    }
}
