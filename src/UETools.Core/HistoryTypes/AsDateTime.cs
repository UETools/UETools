using System;
using UETools.Core.Enums;

namespace UETools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class AsDateTime : FTextHistory
        {
            public override void Deserialize(FArchive reader)
            {
                base.Deserialize(reader);

                reader.Read(out _sourceDateTime);
                reader.ReadUnsafe(out _dateStyle);
                reader.ReadUnsafe(out _timeStyle);
                reader.Read(out _timeZone);
                reader.Read(out _cultureName);
            }

            public override string ToString()
            {
                return new DateTime((long)_sourceDateTime).ToString();
            }

            private ulong _sourceDateTime;
            private DateTimeStyle _dateStyle;
            private DateTimeStyle _timeStyle;
            private FString _timeZone = null!;
            private FString _cultureName = null!;
        }
    }
}
