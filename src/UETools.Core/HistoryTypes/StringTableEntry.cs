using System;

namespace UETools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class StringTableEntry : FTextHistory
        {
            public override FArchive Serialize(FArchive archive)
            {
                archive.Read(ref _tableId).Read(ref _key);
                // TODO: Read object 'TableID' as StringTable, get value for 'Key'
                throw new NotImplementedException($"{nameof(StringTableEntry)} parsing not implemented.");
                return archive;
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
