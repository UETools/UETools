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

        public override FArchive Serialize(FArchive archive)
        {
            if (ItemCount.HasValue)
                archive.Read(ref _nameEntries, ItemCount.Value);
            else
                archive.Read(ref _nameEntries);

            Items.AddRange(_nameEntries.Select(entry => entry.Name));

            // Add table to dictionary after it's filled
            return base.Serialize(archive);
        }

        private List<NameEntrySerialized> _nameEntries = null!;
    }
}
