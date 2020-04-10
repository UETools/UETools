namespace UETools.Objects.KismetVM
{
    /// <summary>
    /// Bytecode instruction values for casting.
    /// </summary>
    /// <remarks>Should be kept in sync with <see href="https://github.com/EpicGames/UnrealEngine/blob/master/Engine/Source/Runtime/CoreUObject/Public/UObject/Script.h#L162">EExprToken enum</see></remarks>
    //[EnumChecker("https://docs.unrealengine.com/en-US/API/Runtime/CoreUObject/UObject/ECastToken/index.html")]
    public enum ECastToken : byte
    {
        /// <summary>
        /// Cast object expression to interface.
        /// </summary>
        CST_ObjectToInterface = 0x46,
        /// <summary>
        /// Casts object expression to boolean.
        /// </summary>
        CST_ObjectToBool = 0x47,
        /// <summary>
        /// Casts interface expression to boolean
        /// </summary>
        CST_InterfaceToBool = 0x49,
    }
}