namespace UnrealTools.Core.Enums
{
    internal enum TextFlag
    {
        Transient = (1 << 0),
        CultureInvariant = (1 << 1),
        ConvertedProperty = (1 << 2),
        Immutable = (1 << 3),
        InitializedFromString = (1 << 4),
    }
}