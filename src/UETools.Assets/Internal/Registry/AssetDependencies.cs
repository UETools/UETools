using System.Collections.Generic;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Registry
{
    public partial class AssetDependencies : IUnrealSerializable
    {
        public FArchive Serialize(FArchive archive)
        {
            List<int>? depCounts = default;
            return archive.Read(ref depCounts, (int)EAssetRegistryDependency.MaxCount)
                
                  .Read(ref _hardDependencies, depCounts[(int)EAssetRegistryDependency.Hard])
                  .Read(ref _softDependencies, depCounts[(int)EAssetRegistryDependency.Soft])
                  .Read(ref _searchableNameDependencies, depCounts[(int)EAssetRegistryDependency.SearchableName])
                  .Read(ref _softManageDependencies, depCounts[(int)EAssetRegistryDependency.SoftManage])
                  .Read(ref _hardManageDependencies, depCounts[(int)EAssetRegistryDependency.HardManage])
                  .Read(ref _noneDependencies, depCounts[(int)EAssetRegistryDependency.None]);
        }

        private List<int> _hardDependencies = null!;
        private List<int> _softDependencies = null!;
        private List<int> _searchableNameDependencies = null!;
        private List<int> _softManageDependencies = null!;
        private List<int> _hardManageDependencies = null!;
        private List<int> _noneDependencies = null!;
    }
}
