namespace UnrealTools.Assets.Internal.Registry
{
    public partial class AssetDependencies
    {
        enum EAssetRegistryDependency
        {
            /// <summary>
            /// Dependencies which are required for correct usage of the source asset, and must be loaded at the same time.
            /// </summary>
            Hard = 0,
            /// <summary>
            /// Dependencies which don't need to be loaded for the object to be used (i.e. soft object paths).
            /// </summary>
            Soft = 1,
            /// <summary>
            /// References to specific SearchableNames inside a package.
            /// </summary>
            SearchableName = 2,
            /// <summary>
            /// Indirect management references, these are set through recursion for Primary Assets that manage packages or other primary assets.
            /// </summary>
            SoftManage = 3,
            /// <summary>
            /// Reference that says one object directly manages another object, set when Primary Assets manage things explicitly.
            /// </summary>
            HardManage = 4,
            /// <summary>
            /// 
            /// </summary>
            None = 5,

            MaxCount = 6
        }
    }
}
