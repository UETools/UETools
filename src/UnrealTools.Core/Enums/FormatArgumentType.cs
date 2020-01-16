namespace UnrealTools.Core.Enums
{
    internal enum FormatArgumentType : byte
    {
        Int,
        UInt,
        Float,
        Double,
        Text,
        Gender,

        // Add new enum types at the end only! They are serialized by index.
    };
}