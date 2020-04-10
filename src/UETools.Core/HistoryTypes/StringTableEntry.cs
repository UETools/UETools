using System;

namespace UETools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class StringTableEntry : FTextHistory
        {
            public override void Deserialize(FArchive reader)
            {
                base.Deserialize(reader);

                reader.Read(out _tableId);
                reader.Read(out _key);
                // Read object 'TableID' as StringTable, get value for 'Key'
                throw new NotImplementedException($"{nameof(StringTableEntry)} parsing not implemented.");
            }

            public override string ToString()
            {
                return base.ToString();
            }

            private FName _tableId = null!;
            private FString _key = null!;
        }
    }
}
