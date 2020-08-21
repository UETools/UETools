using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Objects.Package
{
    [UnrealTable(TableName)]
    public sealed class ImportTable : UnrealTable<ObjectImport>
    {
        internal const string TableName = "Imports";
        public ImportTable() : base() { }
        public ImportTable(int length) : base(length) { }

        public override FArchive Serialize(FArchive reader)
        {
            base.Serialize(reader);
            if (ItemCount.HasValue)
                reader.Read(ref _entries, ItemCount.Value);
            else
                reader.Read(ref _entries);

            return reader;
        }
    }
}
