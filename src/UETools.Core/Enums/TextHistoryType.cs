using UETools.Core.HistoryTypes;

namespace UETools.Core.Enums
{
    public enum TextHistoryType : sbyte
    {
        [LinkedType(typeof(FTextHistory.None))]
        None = -1,
        [LinkedType(typeof(FTextHistory.Base))]
        Base = 0,
        [LinkedType(typeof(FTextHistory.NamedFormat))]
        NamedFormat,
        [LinkedType(typeof(FTextHistory.OrderedFormat))]
        OrderedFormat,
        [LinkedType(typeof(FTextHistory.ArgumentFormat))]
        ArgumentFormat,
        [LinkedType(typeof(FTextHistory.AsNumber))]
        AsNumber,
        [LinkedType(typeof(FTextHistory.AsPercent))]
        AsPercent,
        [LinkedType(typeof(FTextHistory.AsCurrency))]
        AsCurrency,
        [LinkedType(typeof(FTextHistory.AsDate))]
        AsDate,
        [LinkedType(typeof(FTextHistory.AsTime))]
        AsTime,
        [LinkedType(typeof(FTextHistory.AsDateTime))]
        AsDateTime,
        [LinkedType(typeof(FTextHistory.Transform))]
        Transform,
        [LinkedType(typeof(FTextHistory.StringTableEntry))]
        StringTableEntry,

        // Add new enum types at the end only! They are serialized by index.
    };
}