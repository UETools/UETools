using System;
//using UnrealTools.Core.CodeGeneration.Attributes;

namespace UETools.Core
{
    /// <summary>
    /// Flags of the package.
    /// </summary>
    /// <remarks>Should be kept in sync with <see href="https://github.com/EpicGames/UnrealEngine/blob/master/Engine/Source/Runtime/CoreUObject/Public/UObject/ObjectMacros.h#L102">EPackageFlags</see>.</remarks>
    [Flags]
    //[EnumUpdater("https://docs.unrealengine.com/en-US/API/Runtime/CoreUObject/UObject/EPackageFlags/index.html")]
    public enum EPackageFlags : uint
    {
        /// <summary>
        /// No flags.
        /// </summary>
        None = 0x00000000,
        /// <summary>
        /// Newly created package, not saved yet. In editor only.
        /// </summary>
        NewlyCreated = 0x00000001,
        /// <summary>
        /// Purely optional for clients.
        /// </summary>
        ClientOptional = 0x00000002,
        /// <summary>
        /// Only needed on the server side.
        /// </summary>
        ServerSideOnly = 0x00000004,
        /// <summary>
        /// This package is from "compiled in" classes.
        /// </summary>
        CompiledIn = 0x00000010,
        /// <summary>
        /// This package was loaded just for the purposes of diffing.
        /// </summary>
        ForDiffing = 0x00000020,
        /// <summary>
        /// This is editor-only package (for example: editor module script package).
        /// </summary>
        EditorOnly = 0x00000040,
        /// <summary>
        /// Developer module.
        /// </summary>
        Developer = 0x00000080,
        /// <summary>
        /// Contains map data (UObjects only referenced by a single ULevel) but is stored in a different package.
        /// </summary>
        ContainsMapData = 0x00004000,
        /// <summary>
        /// Client needs to download this package.
        /// </summary>
        Need = 0x00008000,
        /// <summary>
        /// Package is currently being compiled.
        /// </summary>
        Compiling = 0x00010000,
        /// <summary>
        /// Set if the package contains a ULevel / UWorld object.
        /// </summary>
        ContainsMap = 0x00020000,
        /// <summary>
        /// Set if the package contains any data to be gathered by localization.
        /// </summary>
        RequiresLocalizationGather = 0x00040000,
        /// <summary>
        /// Set if the archive serializing this package cannot use lazy loading.
        /// </summary>
        DisallowLazyLoading = 0x00080000,
        /// <summary>
        /// Set if the package was created for the purpose of PIE.
        /// </summary>
        PlayInEditor = 0x00100000,
        /// <summary>
        /// Package is allowed to contain UClass objects.
        /// </summary>
        ContainsScript = 0x00200000,
        /// <summary>
        /// Editor should not export asset in this package.
        /// </summary>
        DisallowExport = 0x00400000,
        /// <summary>
        /// This package is reloading in the cooker, try to avoid getting data we will never need. We won't save this package.
        /// </summary>
        ReloadingForCooker = 0x40000000,
        /// <summary>
        /// Package has editor-only data filtered.
        /// </summary>
        FilterEditorOnly = 0x80000000,

        /// <summary>
        /// Flag mask that indicates if this package is a package that exists in memory only.
        /// </summary>
        InMemoryOnly = CompiledIn | NewlyCreated
    }
}
