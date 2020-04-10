using System;

namespace UETools.Objects.Enums
{
    /// <summary>Flags describing an object instance</summary>
    [Flags]
    internal enum EObjectFlags : uint
    {
        /// <summary>
        /// No flags, used to avoid a cast.
        /// </summary>
        RF_NoFlags = 0x00000000,

        // This first group of flags mostly has to do with what kind of object it is. Other than transient, these are the persistent object flags.
        // The garbage collector also tends to look at these.
        /// <summary>
        /// Object is visible outside its package.
        /// </summary>
        RF_Public = 0x00000001,
        /// <summary>
        /// Keep object around for editing even if unreferenced.
        /// </summary>
        RF_Standalone = 0x00000002,
        /// <summary>
        /// Object (UField) will be marked as native on construction (DO NOT USE THIS FLAG in HasAnyFlags() etc)
        /// </summary>
        RF_MarkAsNative = 0x00000004,
        /// <summary>
        /// Object is transactional
        /// .</summary>
        RF_Transactional = 0x00000008,
        /// <summary>
        /// This object is its class's default object.
        /// </summary>
        RF_ClassDefaultObject = 0x00000010,
        /// <summary>
        /// This object is a template for another object - treat like a class default object.
        /// </summary>
        RF_ArchetypeObject = 0x00000020,
        /// <summary>
        /// Don't save object.
        /// </summary>
        RF_Transient = 0x00000040,

        // This group of flags is primarily concerned with garbage collection.
        /// <summary>
        /// Object will be marked as root set on construction and not be garbage collected, even if unreferenced (DO NOT USE THIS FLAG in HasAnyFlags() etc)
        /// </summary>
        RF_MarkAsRootSet = 0x00000080,
        /// <summary>
        /// This is a temp user flag for various utilities that need to use the garbage collector. The garbage collector itself does not interpret it.
        /// </summary>
        RF_TagGarbageTemp = 0x00000100,

        // The group of flags tracks the stages of the lifetime of a uobject
        /// <summary>
        /// This object has not completed its initialization process. Cleared when ~FObjectInitializer completes.
        /// </summary>
        RF_NeedInitialization = 0x00000200,
        /// <summary>
        /// During load, indicates object needs loading.
        /// </summary>
        RF_NeedLoad = 0x00000400,
        /// <summary>
        /// Keep this object during garbage collection because it's still being used by the cooker.
        /// </summary>
        RF_KeepForCooker = 0x00000800,
        /// <summary>
        /// Object needs to be postloaded.
        /// </summary>
        RF_NeedPostLoad = 0x00001000,
        /// <summary>
        /// During load, indicates that the object still needs to instance subobjects and fixup serialized component references.
        /// </summary>
        RF_NeedPostLoadSubobjects = 0x00002000,
        /// <summary>
        /// Object has been consigned to oblivion due to its owner package being reloaded, and a newer version currently exists.
        /// </summary>
        RF_NewerVersionExists = 0x00004000,
        /// <summary>
        /// BeginDestroy has been called on the object.
        /// </summary>
        RF_BeginDestroyed = 0x00008000,
        /// <summary>
        /// FinishDestroy has been called on the object.
        /// </summary>
        RF_FinishDestroyed = 0x00010000,

        // Misc. Flags
        /// <summary>
        /// Flagged on UObjects that are used to create UClasses (e.g. Blueprints) while they are regenerating their UClass on load (See FLinkerLoad::CreateExport())
        /// </summary>
        RF_BeingRegenerated = 0x00020000,
        /// <summary>
        /// Flagged on subobjects that are defaults.
        /// </summary>
        RF_DefaultSubObject = 0x00040000,
        /// <summary>
        /// Flagged on UObjects that were loaded.
        /// </summary>
        RF_WasLoaded = 0x00080000,
        /// <summary>
        /// Do not export object to text form (e.g. copy/paste). Generally used for sub-objects that can be regenerated from data in their parent object.
        /// </summary>
        RF_TextExportTransient = 0x00100000,
        /// <summary>
        /// Object has been completely serialized by linkerload at least once. DO NOT USE THIS FLAG, It should be replaced with RF_WasLoaded.
        /// </summary>
        RF_LoadCompleted = 0x00200000,
        /// <summary>
        /// Archetype of the object can be in its super class.
        /// </summary>
        RF_InheritableComponentTemplate = 0x00400000,
        /// <summary>
        /// Object should not be included in any type of duplication (copy/paste, binary duplication, etc).
        /// </summary>
        RF_DuplicateTransient = 0x00800000,
        /// <summary>
        /// References to this object from persistent function frame are handled as strong ones.
        /// </summary>
        RF_StrongRefOnFrame = 0x01000000,
        /// <summary>
        /// Object should not be included for duplication unless it's being duplicated for a PIE session.
        /// </summary>
        RF_NonPIEDuplicateTransient = 0x02000000,
        /// <summary>
        /// Field Only. Dynamic field - doesn't get constructed during static initialization, can be constructed multiple times.
        /// </summary>
        RF_Dynamic = 0x04000000,
        /// <summary>
        /// This object was constructed during load and will be loaded shortly.
        /// </summary>
        RF_WillBeLoaded = 0x08000000,

        /// <summary>
        /// Flags to load from Unrealfiles.
        /// </summary>
        RF_Load = RF_Public | RF_Standalone | RF_Transactional | RF_ClassDefaultObject | RF_ArchetypeObject | RF_DefaultSubObject | RF_TextExportTransient | RF_InheritableComponentTemplate | RF_DuplicateTransient | RF_NonPIEDuplicateTransient,
        /// <summary>
        /// Sub-objects will inherit these flags from their SuperObject.
        /// </summary>
        RF_PropagateToSubObjects = RF_Public | RF_ArchetypeObject | RF_Transactional | RF_Transient,
        /// <summary>
        /// All flags, used mainly for error checking.
        /// </summary>
        RF_AllFlags = 0x0fffffff,
    }
}
