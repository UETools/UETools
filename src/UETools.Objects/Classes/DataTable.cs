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
    public sealed class DataTable : UObject, IUnrealDeserializable, IUnrealReadable
    {
        public TableItems Rows { get; } = new TableItems();

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            ReadRows(reader);
        }

        private void ReadRows(FArchive reader)
        {
            if (base["RowStruct"] is null)
                return;

            reader.Read(out int RowCount);
            for (var i = 0; i < RowCount; i++)
                ReadDataTableRow(reader);
        }

        private void ReadDataTableRow(FArchive reader)
        {
            reader.Read(out FName RowName);
            Rows.Add(new KeyValuePair<string, TaggedObject>(RowName, new TaggedObject(reader)));
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
