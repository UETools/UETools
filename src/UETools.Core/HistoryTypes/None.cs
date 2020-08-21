namespace UETools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class None : FTextHistory
        {
            public override FArchive Serialize(FArchive reader) => reader;
            public override string ToString() => string.Empty;
        }
    }
}
