using System;
using System.Collections.Generic;
using System.Linq;
using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Enums;

namespace UETools.Objects.Package
{
    [UnrealTable(TableName)]
    public sealed class ExportTable : UnrealTable<ObjectExport>
    {
        internal const string TableName = "Exports";
        public ExportTable() : base() { }
        public ExportTable(int length) : base(length) { }

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            if (ItemCount.HasValue)
                reader.Read(out _entries, ItemCount.Value);
            else
                reader.Read(out _entries);
        }


        public ObjectExport? GetClass() => GetCDO()?.Class as ObjectExport;
        public IEnumerable<ObjectExport> GetStandalone() => Items.Where(ex => ex.ObjectFlags.HasFlag(EObjectFlags.RF_Standalone));
        public IEnumerable<ObjectExport> GetAssets() => Items.Where(ex => ex.IsAsset);
        public ObjectExport? GetCDO() => Items.FirstOrDefault(ex => ex.ObjectFlags.HasFlag(EObjectFlags.RF_ClassDefaultObject));
    }
}
