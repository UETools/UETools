using System;
using UETools.Core.Enums;

namespace UETools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class AsTime : FTextHistory
        {
            public override FArchive Serialize(FArchive reader) 
                => reader.Read(ref _sourceDateTime)
                         .ReadUnsafe(ref _timeStyle)
                         .Read(ref _timeZone)
                         .Read(ref _cultureName);

            public override string ToString()
            {
                if (_timeZone is null || _cultureName is null)
                    NotDeserializedException.Throw();

                return new DateTime((long)_sourceDateTime).TimeOfDay.ToString();
            }

            private ulong _sourceDateTime;
            private DateTimeStyle _timeStyle;
            private FString _timeZone = null!;
            private FString _cultureName = null!;
        }
    }
}
