using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Classes.Internal;

namespace UETools.Objects.Classes
{
    using TableItems = List<KeyValuePair<string, TaggedObject>>;
    public sealed class DataTable : UObject, IUnrealSerializable, IUnrealReadable
    {
        public TableItems Rows { get; } = new TableItems();

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);
            ReadRows(archive);
            return archive;
        }

        private void ReadRows(FArchive reader)
        {
            if (base["RowStruct"] is null)
                return;
            int rowCount = 0;
            reader.Read(ref rowCount);
            for (var i = 0; i < rowCount; i++)
                ReadDataTableRow(reader);
        }

        private void ReadDataTableRow(FArchive archive)
        {
            FName? RowName = default;
            archive.Read(ref RowName);
            Rows.Add(new KeyValuePair<string, TaggedObject>(RowName, new TaggedObject(archive)));
        }

        public override void ReadTo(IndentedTextWriter writer)
        {
            base.ReadTo(writer);
            foreach (var row in Rows)
            {
                writer.WriteLine();
                writer.WriteLine($"Row {row.Key}:");
                row.Value.ReadTo(writer);
            }
        }
    }
}
