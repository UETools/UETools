using System;

namespace UnrealTools.Objects.Enums
{
    [Flags]
    internal enum EClassFlags : uint
    {
        None = 0x00000000u,
        Abstract = 0x00000001u,
        DefaultConfig = 0x00000002u,
        Config = 0x00000004u,
        Transient = 0x00000008u,
        Parsed = 0x00000010u,
        MatchedSerializers = 0x00000020u,
        AdvancedDisplay = 0x00000040u,
        Native = 0x00000080u,
        NoExport = 0x00000100u,
        NotPlaceable = 0x00000200u,
        PerObjectConfig = 0x00000400u,
        ReplicationDataIsSetUp = 0x00000800u,
        EditInlineNew = 0x00001000u,
        CollapseCategories = 0x00002000u,
        Interface = 0x00004000u,
        CustomConstructor = 0x00008000u,
        Const = 0x00010000u,
        LayoutChanging = 0x00020000u,
        CompiledFromBlueprint = 0x00040000u,
        MinimalAPI = 0x00080000u,
        RequiredAPI = 0x00100000u,
        DefaultToInstanced = 0x00200000u,
        TokenStreamAssembled = 0x00400000u,
        HasInstancedReference = 0x00800000u,
        Hidden = 0x01000000u,
        Deprecated = 0x02000000u,
        HideDropDown = 0x04000000u,
        GlobalUserConfig = 0x08000000u,
        Intrinsic = 0x10000000u,
        ucted = 0x20000000u,
        ConfigDoNotCheckDefaults = 0x40000000u,
        NewerVersionExists = 0x80000000u,

        /// <summary>
        /// Flags to inherit from base class.
        /// </summary>
        Inherit = Transient | DefaultConfig | Config | PerObjectConfig | ConfigDoNotCheckDefaults | NotPlaceable | Const | HasInstancedReference | Deprecated | DefaultToInstanced | GlobalUserConfig,
        /// <summary>
        /// These flags will be cleared by the compiler when the class is parsed during script compilation.
        /// </summary>
        RecompilerClear = Inherit | Abstract | NoExport | Native | Intrinsic | TokenStreamAssembled,
        /// <summary>
        /// These flags will be cleared by the compiler when the class is parsed during script compilation.
        /// </summary>
        ShouldNeverBeLoaded = Native | Intrinsic | TokenStreamAssembled,
        /// <summary>
        /// These flags will be inherited from the base class only for non-intrinsic classes.
        /// </summary>
        ScriptInherit = Inherit | EditInlineNew | CollapseCategories,
        /// <summary>
        /// This is used as a mask for the flags put into generated code for "compiled in" classes.
        /// </summary>
        SaveInCompiledInClasses = Abstract | DefaultConfig | GlobalUserConfig | Config | Transient | Native | NotPlaceable | PerObjectConfig | ConfigDoNotCheckDefaults | EditInlineNew | CollapseCategories | Interface | DefaultToInstanced | HasInstancedReference | Hidden | Deprecated | HideDropDown | Intrinsic | AdvancedDisplay | Const | MinimalAPI | RequiredAPI | MatchedSerializers

    }
}
