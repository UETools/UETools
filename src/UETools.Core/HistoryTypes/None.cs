namespace UETools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class None : FTextHistory
        {
            public override FArchive Serialize(FArchive archive) => archive;
            public override string ToString() => string.Empty;
        }
    }
}
