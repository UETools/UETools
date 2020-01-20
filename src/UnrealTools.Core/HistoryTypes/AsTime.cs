using System;
using UnrealTools.Core.Enums;

namespace UnrealTools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class AsTime : FTextHistory
        {
            public override void Deserialize(FArchive reader)
            {
                base.Deserialize(reader);

                reader.Read(out _sourceDateTime);
                reader.ReadUnsafe(out _timeStyle);
                reader.Read(out _timeZone);
                reader.Read(out _cultureName);
            }

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
