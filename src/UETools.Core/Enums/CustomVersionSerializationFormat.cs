namespace UETools.Core.Enums
{
    public enum CustomVersionSerializationFormat
    {
        Unknown,
        Guids,
        Enums,
        Optimized,

        // Add new versions above this comment
        CustomVersion_Automatic_Plus_One,
        Latest = CustomVersion_Automatic_Plus_One - 1
    }
}
