using System.Collections.Generic;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Registry
{
    public partial class AssetDependencies : IUnrealDeserializable
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out List<int> depCounts, (int)EAssetRegistryDependency.MaxCount);

            reader.Read(out _hardDependencies, depCounts[(int)EAssetRegistryDependency.Hard]);
            reader.Read(out _softDependencies, depCounts[(int)EAssetRegistryDependency.Soft]);
            reader.Read(out _searchableNameDependencies, depCounts[(int)EAssetRegistryDependency.SearchableName]);
            reader.Read(out _softManageDependencies, depCounts[(int)EAssetRegistryDependency.SoftManage]);
            reader.Read(out _hardManageDependencies, depCounts[(int)EAssetRegistryDependency.HardManage]);
            reader.Read(out _noneDependencies, depCounts[(int)EAssetRegistryDependency.None]);
        }

        private List<int> _hardDependencies = null!;
        private List<int> _softDependencies = null!;
        private List<int> _searchableNameDependencies = null!;
        private List<int> _softManageDependencies = null!;
        private List<int> _hardManageDependencies = null!;
        private List<int> _noneDependencies = null!;
    }
}
