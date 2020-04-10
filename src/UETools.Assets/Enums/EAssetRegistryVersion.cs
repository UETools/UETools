//using UnrealTools.CodeGen.Attributes;

namespace UETools.Assets.Enums
{
    //[EnumUpdater("https://docs.unrealengine.com/en-US/API/Runtime/AssetRegistry/FAssetRegistryVersion/Type/index.html")]
    public enum EAssetRegistryVersion : int
    {
        /// <summary>
        /// From before file versioning was implemented.
        /// </summary>
        PreVersioning = 0,
        /// <summary>
        /// The first version of the runtime asset registry to include file versioning.
        /// </summary>
        HardSoftDependencies,
        /// <summary>
        /// Added FAssetRegistryState and support for piecemeal serialization.
        /// </summary>
        AddAssetRegistryState,
        /// <summary>
        /// AssetData serialization format changed, versions before this are not readable.
        /// </summary>
        ChangedAssetData,
        /// <summary>
        /// Removed MD5 hash from package data.
        /// </summary>
        RemovedMD5Hash,
        /// <summary>
        /// Added hard/soft manage references.
        /// </summary>
        AddedHardManage,
        /// <summary>
        /// Added MD5 hash of cooked package to package data.
        /// </summary>
        AddedCookedMD5Hash,

        /// <summary>
        /// Last version implemented.
        /// </summary>
        VersionPlusOne,
        /// <summary>
        /// Latest version implemented.
        /// </summary>
        LatestVersion = VersionPlusOne - 1
    }
}
