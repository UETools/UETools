using System.Collections.Generic;
using System.Linq;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Core
{
    [UnrealTable("Names")]
    public sealed class NameTable : UnrealTable<FString>
    {
        public NameTable() { }
        public NameTable(int length) : base(length) { }

        public override void Deserialize(FArchive reader)
        {
            if (ItemCount.HasValue)
                reader.Read(out _nameEntries, ItemCount.Value);
            else
                reader.Read(out _nameEntries);

            Items.AddRange(_nameEntries.Select(entry => entry.Name));

            // Add table to dictionary after it's filled
            base.Deserialize(reader);
        }

        private List<NameEntrySerialized> _nameEntries = null!;
    }
}
