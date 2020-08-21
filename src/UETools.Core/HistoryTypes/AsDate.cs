using System;
using UETools.Core.Enums;

namespace UETools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class AsDate : FTextHistory
        {
            public AsDate() { }
            public AsDate(DateTime date)
            {
                _sourceDateTime = (ulong)date.ToUniversalTime().Date.Ticks;
                _dateStyle = DateTimeStyle.Default;
            }
            public override FArchive Serialize(FArchive reader)
            {
                reader.Read(ref _sourceDateTime)
                      .ReadUnsafe(ref _dateStyle);
                if (reader.Version >= UE4Version.VER_UE4_FTEXT_HISTORY_DATE_TIMEZONE)
                    reader.Read(ref _timeZone);

                return reader.Read(ref _cultureName);
            }

            public override string ToString()
            {
                return new DateTime((long)_sourceDateTime).Date.ToString();
            }

            private ulong _sourceDateTime;
            private DateTimeStyle _dateStyle;
            private FString? _timeZone;
            private FString _cultureName = null!;
        }
    }
}
