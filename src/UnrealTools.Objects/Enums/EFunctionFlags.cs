using System;

namespace UnrealTools.Objects.Enums
{
    [Flags]
    internal enum EFunctionFlags : uint
    {
        // Function flags.
        None = 0x00000000,

        Final = 0x00000001,    // Function is final (prebindable, non-overridable function).
        RequiredAPI = 0x00000002,  // Indicates this function is DLL exported/imported.
        BlueprintAuthorityOnly = 0x00000004,   // Function will only run if the object has network authority
        BlueprintCosmetic = 0x00000008,   // Function is cosmetic in nature and should not be invoked on dedicated servers
        // FUNC_ = 0x00000010,   // unused.
        // FUNC_ = 0x00000020,   // unused.
        Net = 0x00000040,   // Function is network-replicated.
        NetReliable = 0x00000080,   // Function should be sent reliably on the network.
        NetRequest = 0x00000100,   // Function is sent to a net service
        Exec = 0x00000200, // Executable from command line.
        Native = 0x00000400,   // Native function.
        Event = 0x00000800,   // Event function.
        NetResponse = 0x00001000,   // Function response from a net service
        Static = 0x00002000,   // Static function.
        NetMulticast = 0x00004000, // Function is networked multicast Server -> All Clients
        UbergraphFunction = 0x00008000,
        MulticastDelegate = 0x00010000,    // Function is a multi-cast delegate signature (also requires FUNC_Delegate to be set!)
        Public = 0x00020000,   // Function is accessible in all classes (if overridden, parameters must remain unchanged).
        Private = 0x00040000,  // Function is accessible only in the class it is defined in (cannot be overridden, but function name may be reused in subclasses.  IOW: if overridden, parameters don't need to match, and Super.Func() cannot be accessed since it's private.)
        Protected = 0x00080000,    // Function is accessible only in the class it is defined in and subclasses (if overridden, parameters much remain unchanged).
        Delegate = 0x00100000, // Function is delegate signature (either single-cast or multi-cast, depending on whether FUNC_MulticastDelegate is set.)
        NetServer = 0x00200000,    // Function is executed on servers (set by replication code if passes check)
        HasOutParms = 0x00400000,  // function has out (pass by reference) parameters
        HasDefaults = 0x00800000,  // function has structs that contain defaults
        NetClient = 0x01000000,    // function is executed on clients
        DLLImport = 0x02000000,    // function is imported from a DLL
        BlueprintCallable = 0x04000000,    // function can be called from blueprint code
        BlueprintEvent = 0x08000000,   // function can be overridden/implemented from a blueprint
        BlueprintPure = 0x10000000,    // function can be called from blueprint code, and is also pure (produces no side effects). If you set this, you should set FUNC_BlueprintCallable as well.
        EditorOnly = 0x20000000,   // function can only be called from an editor scrippt.
        Const = 0x40000000,    // function can be called from blueprint code, and only reads state (never writes state)
        NetValidate = 0x80000000,  // function must supply a _Validate implementation

        AllFlags = 0xFFFFFFFF
    }
}
