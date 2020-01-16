//using UnrealTools.CodeGen.Attributes;

namespace UnrealTools.KismetVM
{
    /// <summary>
    /// Values represent specific Kismet bytecode instructions.
    /// </summary>
    /// <remarks>Should be kept in sync with <see href="https://github.com/EpicGames/UnrealEngine/blob/master/Engine/Source/Runtime/CoreUObject/Public/UObject/Script.h#L162">EExprToken enum</see></remarks>
    //[EnumUpdater("https://docs.unrealengine.com/en-US/API/Runtime/CoreUObject/UObject/EExprToken/index.html")]
    public enum EExprToken : byte
    {
        /// <summary>
        /// A local variable.
        /// </summary>
        EX_LocalVariable = 0x00,
        /// <summary>
        /// An object variable.
        /// </summary>
        EX_InstanceVariable = 0x01,
        /// <summary>
        /// Default variable for a class context.
        /// </summary>
        EX_DefaultVariable = 0x02,
        /// <summary>
        /// Return from function.
        /// </summary>
        EX_Return = 0x04,
        /// <summary>
        /// Goto a local address in code.
        /// </summary>
        EX_Jump = 0x06,
        /// <summary>
        /// Goto if not expression.
        /// </summary>
        EX_JumpIfNot = 0x07,
        /// <summary>
        /// Assertion.
        /// </summary>
        EX_Assert = 0x09,
        /// <summary>
        /// No operation.
        /// </summary>
        EX_Nothing = 0x0B,
        /// <summary>
        /// Assign an arbitrary size value to a variable.
        /// </summary>
        EX_Let = 0x0F,
        /// <summary>
        /// Class default object context.
        /// </summary>
        EX_ClassContext = 0x12,
        /// <summary>
        /// Metaclass cast.
        /// </summary>
        EX_MetaCast = 0x13,
        /// <summary>
        /// Let boolean variable.
        /// </summary>
        EX_LetBool = 0x14,
        /// <summary>
        /// End of default value for optional function parameter.
        /// </summary>
        EX_EndParmValue = 0x15,
        /// <summary>
        /// End of function call parameters.
        /// </summary>
        EX_EndFunctionParms = 0x16,
        /// <summary>
        /// Self object.
        /// </summary>
        EX_Self = 0x17,
        /// <summary>
        /// Skippable expression.
        /// </summary>
        EX_Skip = 0x18,
        /// <summary>
        /// Call a function through an object context.
        /// </summary>
        EX_Context = 0x19,
        /// <summary>
        /// Call a function through an object context (can fail silently if the context is NULL; only generated for functions that don't have output or return values).
        /// </summary>
        EX_Context_FailSilent = 0x1A,
        /// <summary>
        /// A function call with parameters.
        /// </summary>
        EX_VirtualFunction = 0x1B,
        /// <summary>
        /// A prebound function call with parameters.
        /// </summary>
        EX_FinalFunction = 0x1C,
        /// <summary>
        /// Int constant.
        /// </summary>
        EX_IntConst = 0x1D,
        /// <summary>
        /// Floating point constant.
        /// </summary>
        EX_FloatConst = 0x1E,
        /// <summary>
        /// String constant.
        /// </summary>
        EX_StringConst = 0x1F,
        /// <summary>
        /// An object constant.
        /// </summary>
        EX_ObjectConst = 0x20,
        /// <summary>
        /// A name constant.
        /// </summary>
        EX_NameConst = 0x21,
        /// <summary>
        /// A rotation constant.
        /// </summary>
        EX_RotationConst = 0x22,
        /// <summary>
        /// A vector constant.
        /// </summary>
        EX_VectorConst = 0x23,
        /// <summary>
        /// A byte constant.
        /// </summary>
        EX_ByteConst = 0x24,
        /// <summary>
        /// Zero.
        /// </summary>
        EX_IntZero = 0x25,
        /// <summary>
        /// One.
        /// </summary>
        EX_IntOne = 0x26,
        /// <summary>
        /// Bool True.
        /// </summary>
        EX_True = 0x27,
        /// <summary>
        /// Bool False.
        /// </summary>
        EX_False = 0x28,
        /// <summary>
        /// FText constant.
        /// </summary>
        EX_TextConst = 0x29,
        /// <summary>
        /// NoObject, <see langword="null"/> value.
        /// </summary>
        EX_NoObject = 0x2A,
        /// <summary>
        /// A transform constant.
        /// </summary>
        EX_TransformConst = 0x2B,
        /// <summary>
        /// Int constant that requires 1 byte.
        /// </summary>
        EX_IntConstByte = 0x2C,
        /// <summary>
        /// A <see langword="null"/> interface (similar to <seealso cref="EX_NoObject"/>, but for interfaces).
        /// </summary>
        EX_NoInterface = 0x2D,
        /// <summary>
        /// Safe dynamic class casting.
        /// </summary>
        EX_DynamicCast = 0x2E,
        /// <summary>
        /// An arbitrary UStruct constant.
        /// </summary>
        EX_StructConst = 0x2F,
        /// <summary>
        /// End of UStruct constant.
        /// </summary>
        EX_EndStructConst = 0x30,
        /// <summary>
        /// Set the value of arbitrary array.
        /// </summary>
        EX_SetArray = 0x31,
        /// <summary>
        /// End of the value list of arbitrary array
        /// </summary>
        EX_EndArray = 0x32,
        /// <summary>
        /// Unicode string constant.
        /// </summary>
        EX_UnicodeStringConst = 0x34,
        /// <summary>
        /// 64-bit integer constant.
        /// </summary>
        EX_Int64Const = 0x35,
        /// <summary>
        /// 64-bit unsigned integer constant.
        /// </summary>
        EX_UInt64Const = 0x36,
        /// <summary>
        /// A casting operator for primitives which reads the type as the subsequent byte of <see cref="ECastToken"/>.
        /// </summary>
        EX_PrimitiveCast = 0x38,
        /// <summary>
        /// Initialization of set elements.
        /// </summary>
        EX_SetSet = 0x39,
        /// <summary>
        /// End of set elements initialization.
        /// </summary>
        EX_EndSet = 0x3A,
        /// <summary>
        /// Initialization of map elements.
        /// </summary>
        EX_SetMap = 0x3B,
        /// <summary>
        /// End of map elements initialization.
        /// </summary>
        EX_EndMap = 0x3C,
        /// <summary>
        /// Initialization of set with constant arguments.
        /// </summary>
        EX_SetConst = 0x3D,
        /// <summary>
        /// End of set constants elements.
        /// </summary>
        EX_EndSetConst = 0x3E,
        /// <summary>
        /// Initialization of map with constant arguments.
        /// </summary>
        EX_MapConst = 0x3F,
        /// <summary>
        /// End of map constants elements.
        /// </summary>
        EX_EndMapConst = 0x40,
        /// <summary>
        /// Context expression to address a property within a struct.
        /// </summary>
        EX_StructMemberContext = 0x42,
        /// <summary>
        /// Assignment to a multi-cast delegate.
        /// </summary>
        EX_LetMulticastDelegate = 0x43,
        /// <summary>
        /// Assignment to a delegate.
        /// </summary>
        EX_LetDelegate = 0x44,
        /// <summary>
        /// Special instructions to quickly call a virtual function that we know is going to run only locally.
        /// </summary>
        EX_LocalVirtualFunction = 0x45,
        /// <summary>
        /// Special instructions to quickly call a final function that we know is going to run only locally.
        /// </summary>
        EX_LocalFinalFunction = 0x46,
        /// <summary>
        /// Local out (pass by reference) function parameter.
        /// </summary>
        EX_LocalOutVariable = 0x48,
        /// <summary>
        /// Deprecated opcode.
        /// </summary>
        EX_DeprecatedOp4A = 0x4A,
        /// <summary>
        /// Const reference to a delegate or normal function object.
        /// </summary>
        EX_InstanceDelegate = 0x4B,
        /// <summary>
        /// Push an address on to the execution flow stack for future execution when a <see cref="EX_PopExecutionFlow"/> is executed. Execution continues on normally and doesn't change to the pushed address.
        /// </summary>
        EX_PushExecutionFlow = 0x4C,
        /// <summary>
        /// Continue execution at the last address previously pushed onto the execution flow stack.
        /// </summary>
        EX_PopExecutionFlow = 0x4D,
        /// <summary>
        /// Goto a local address in code, specified by an integer value.
        /// </summary>
        EX_ComputedJump = 0x4E,
        /// <summary>
        /// Continue execution at the last address previously pushed onto the execution flow stack, if the condition is not true.
        /// </summary>
        EX_PopExecutionFlowIfNot = 0x4F,
        /// <summary>
        /// Breakpoint. Only observed in the editor, otherwise it behaves like <see cref="EX_Nothing"/>.
        /// </summary>
        EX_Breakpoint = 0x50,
        /// <summary>
        /// Call a function through a native interface variable.
        /// </summary>
        EX_InterfaceContext = 0x51,
        /// <summary>
        /// Converting an object reference to native interface variable.
        /// </summary>
        EX_ObjToInterfaceCast = 0x52,
        /// <summary>
        /// Last byte in script code.
        /// </summary>
        EX_EndOfScript = 0x53,
        /// <summary>
        /// Converting an interface variable reference to native interface variable.
        /// </summary>
        EX_CrossInterfaceCast = 0x54,
        /// <summary>
        /// Converting an interface variable reference to an object.
        /// </summary>
        EX_InterfaceToObjCast = 0x55,
        /// <summary>
        /// Trace point. Only observed in the editor, otherwise it behaves like <see cref="EX_Nothing"/>.
        /// </summary>
        EX_WireTracepoint = 0x5A,
        /// <summary>
        /// A CodeSizeSkipOffset constant
        /// </summary>
        EX_SkipOffsetConst = 0x5B,
        /// <summary>
        /// Adds a delegate to a multicast delegate's targets.
        /// </summary>
        EX_AddMulticastDelegate = 0x5C,
        /// <summary>
        /// Clears all delegates in a multicast target.
        /// </summary>
        EX_ClearMulticastDelegate = 0x5D,
        /// <summary>
        /// Trace point. Only observed in the editor, otherwise it behaves like <see cref="EX_Nothing"/>.
        /// </summary>
        EX_Tracepoint = 0x5E,
        /// <summary>
        /// Assign to any object ref pointer.
        /// </summary>
        EX_LetObj = 0x5F,
        /// <summary>
        /// Assign to a weak object pointer
        /// </summary>
        EX_LetWeakObjPtr = 0x60,
        /// <summary>
        /// Bind object and name to delegate
        /// </summary>
        EX_BindDelegate = 0x61,
        /// <summary>
        /// Remove a delegate from a multicast delegate's targets.
        /// </summary>
        EX_RemoveMulticastDelegate = 0x62,
        /// <summary>
        /// Call multicast delegate.
        /// </summary>
        EX_CallMulticastDelegate = 0x63,
        /// <summary>
        /// Assign to value on persistent frame.
        /// </summary>
        EX_LetValueOnPersistentFrame = 0x64,
        /// <summary>
        /// Initialization of array with constant elements.
        /// </summary>
        EX_ArrayConst = 0x65,
        /// <summary>
        /// End of constant array element list.
        /// </summary>
        EX_EndArrayConst = 0x66,
        /// <summary>
        /// SoftObject instance, initialized with a constant.
        /// </summary>
        EX_SoftObjectConst = 0x67,
        /// <summary>
        /// Static pure function from on local call space.
        /// </summary>
        EX_CallMath = 0x68,
        /// <summary>
        /// Switch expression.
        /// </summary>
        EX_SwitchValue = 0x69,
        /// <summary>
        /// Instrumentation event.
        /// </summary>
        EX_InstrumentationEvent = 0x6A,
        /// <summary>
        /// Get array element reference.
        /// </summary>
        EX_ArrayGetByRef = 0x6B
    };
}
