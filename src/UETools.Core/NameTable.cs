using System.Collections.Generic;
using System.Linq;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    [UnrealTable("Names")]
    public sealed class NameTable : UnrealTable<FString>
    {
        public NameTable() { }
        public NameTable(int length) : base(length) { }

        public override FArchive Serialize(FArchive reader)
        {
            if (ItemCount.HasValue)
                reader.Read(ref _nameEntries, ItemCount.Value);
            else
                reader.Read(ref _nameEntries);

            Items.AddRange(_nameEntries.Select(entry => entry.Name));

            // Add table to dictionary after it's filled
            return base.Serialize(reader);
        }

        private List<NameEntrySerialized> _nameEntries = null!;
    }
}
