using System;
using UETools.Core.Enums;

namespace UETools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class AsDateTime : FTextHistory
        {
            public override FArchive Serialize(FArchive reader)
                => reader.Read(ref _sourceDateTime)
                         .ReadUnsafe(ref _dateStyle)
                         .ReadUnsafe(ref _timeStyle)
                         .Read(ref _timeZone)
                         .Read(ref _cultureName);

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
