using System;
using UnrealTools.Core.Enums;

namespace UnrealTools.Core.HistoryTypes
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
            public override void Deserialize(FArchive reader)
            {
                base.Deserialize(reader);

                reader.Read(out _sourceDateTime);
                reader.ReadUnsafe(out _dateStyle);
                if (reader.Version >= UE4Version.VER_UE4_FTEXT_HISTORY_DATE_TIMEZONE)
                    reader.Read(out _timeZone);

                reader.Read(out _cultureName);
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
