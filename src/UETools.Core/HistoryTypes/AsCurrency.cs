using UETools.Core.Enums;

namespace UETools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class AsCurrency : FormatNumber
        {
            public override FArchive Serialize(FArchive archive)
            {
                if (archive.Version >= UE4Version.VER_UE4_ADDED_CURRENCY_CODE_TO_FTEXT)
                    archive.Read(ref _currencyCode);

                return base.Serialize(archive);
            }
            public override string ToString() => _currencyCode is null
                ? base.ToString()
                : $"{base.ToString()} {_currencyCode.ToString()}";

            private FString? _currencyCode;
        }
    }
}
