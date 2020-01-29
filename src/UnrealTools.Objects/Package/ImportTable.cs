using UnrealTools.Core;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Objects.Package
{
    [UnrealTable(TableName)]
    public sealed class ImportTable : UnrealTable<ObjectImport>
    {
        internal const string TableName = "Imports";
        public ImportTable() : base() { }
        public ImportTable(int length) : base(length) { }

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            if (ItemCount.HasValue)
                reader.Read(out _entries, ItemCount.Value);
            else
                reader.Read(out _entries);
        }
    }
}
