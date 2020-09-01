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

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);
            if (ItemCount.HasValue)
                archive.Read(ref _entries, ItemCount.Value);
            else
                archive.Read(ref _entries);

            return archive;
        }
    }
}
