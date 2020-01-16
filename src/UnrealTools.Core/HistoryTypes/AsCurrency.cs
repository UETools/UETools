using UnrealTools.Core.Enums;

namespace UnrealTools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class AsCurrency : FormatNumber
        {
            public override void Deserialize(FArchive reader)
            {
                if (reader.Version >= UE4Version.VER_UE4_ADDED_CURRENCY_CODE_TO_FTEXT)
                    reader.Read(out _currencyCode);

                base.Deserialize(reader);
            }
            public override string ToString() => _currencyCode is null
                ? base.ToString()
                : $"{base.ToString()} {_currencyCode.ToString()}";

            private FString? _currencyCode;
        }
    }
}
