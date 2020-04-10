using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UETools.Assets.Enums;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Localization
{
    internal class LocalizationTable : IUnrealDeserializable, IUnrealReadable
    {
        public void Deserialize(FArchive reader)
        {
            var VersionNumber = (ELocResVersion)reader.AssetVersion;
            var IsCompact = VersionNumber >= ELocResVersion.Compact;
            var LocalizedStringArray = new List<LocalizationString>();
            if (IsCompact)
            {
                reader.Read(out long LocalizedStringArrayOffset);
                if (LocalizedStringArrayOffset != -1)
                {
                    var offset = reader.Tell();
                    reader.Seek(LocalizedStringArrayOffset);
                    if (VersionNumber >= ELocResVersion.Optimized)
                    {
                        reader.Read(out LocalizedStringArray);
                    }
                    else
                    {
                        reader.Read(out List<FString> tempStringArray);
                        LocalizedStringArray.AddRange(tempStringArray.Select(t => new LocalizationString(t)));
                    }
                    reader.Seek(offset);
                }
            }

            if (VersionNumber >= ELocResVersion.Optimized)
            {
                reader.Read(out uint EntryCount);
            }

            reader.Read(out int NamespaceCount);
            for (var i = 0; i < NamespaceCount; i++)
            {
                reader.Read(out LocalizationKey _namespace);
                var KeyTable = _namespaces.FindOrAdd(_namespace);
                reader.Read(out int KeyCount);
                for (var j = 0; j < KeyCount; j++)
                {
                    reader.Read(out LocalizationKey _key);
                    var Entry = KeyTable.FindOrAdd(_key);

                    reader.Read(out int hash);
                    Entry.SourceStringHash = hash;
                    if (IsCompact)
                    {
                        reader.Read(out int LocalizedStringIndex);
                        if (LocalizedStringIndex > -1 && LocalizedStringIndex < LocalizedStringArray.Count)
                        {
                            Entry.LocalizedString = LocalizedStringArray[LocalizedStringIndex].Value;
                        }
                    }
                    else
                    {
                        reader.Read(out FString LocalizedString);
                        Entry.LocalizedString = LocalizedString;
                    }
                }
            }
        }

        public void ReadTo(IndentedTextWriter writer)
        {
            foreach (var name in _namespaces.OrderBy(kv => kv.Key.ToString()))
            {
                writer.WriteLine($"BEGIN {name.Key}");
                writer.Indent++;
                foreach (var key in name.Value.OrderBy(kv => kv.Key.ToString()))
                {
                    writer.WriteLine(key.Value.ToString());
                }
                writer.Indent--;
                writer.WriteLine($"END {name.Key}");
                writer.WriteLine();
            }
        }

        public LocalizedEntry? Get(string key, string id)
        {
            if (key is null) throw new ArgumentNullException(nameof(key));
            if (id is null) throw new ArgumentNullException(nameof(id));

            return _namespaces.TryGetValue(key, out var value) && value.TryGetValue(id, out var entry) ? entry : null;
        }

        private Dictionary<LocalizationKey, Dictionary<LocalizationKey, LocalizedEntry>> _namespaces = new Dictionary<LocalizationKey, Dictionary<LocalizationKey, LocalizedEntry>>();
    }
}
