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
    internal class LocalizationTable : IUnrealSerializable, IUnrealReadable
    {
        public FArchive Serialize(FArchive archive)
        {
            var VersionNumber = (ELocResVersion)archive.AssetVersion;
            var IsCompact = VersionNumber >= ELocResVersion.Compact;
            List<LocalizationString> LocalizedStringArray = new List<LocalizationString>();
            if (IsCompact)
            {
                long localizedStringArrayOffset = 0;
                archive.Read(ref localizedStringArrayOffset);
                if (localizedStringArrayOffset != -1)
                {
                    var offset = archive.Tell();
                    archive.Seek(localizedStringArrayOffset);
                    if (VersionNumber >= ELocResVersion.Optimized)
                    {
                        archive.Read(ref LocalizedStringArray!);
                    }
                    else
                    {
                        List<FString>? tempStringArray = default;
                        archive.Read(ref tempStringArray);
                        LocalizedStringArray.AddRange(tempStringArray.Select(t => new LocalizationString(t)));
                    }
                    archive.Seek(offset);
                }
            }

            if (VersionNumber >= ELocResVersion.Optimized)
            {
                uint entryCount = 0;
                archive.Read(ref entryCount);
            }
            int namespaceCount = 0;
            archive.Read(ref namespaceCount);
            for (var i = 0; i < namespaceCount; i++)
            {
                LocalizationKey? _namespace = default;
                archive.Read(ref _namespace);
                var KeyTable = _namespaces.FindOrAdd(_namespace);
                int keyCount = 0;
                archive.Read(ref keyCount);
                for (var j = 0; j < keyCount; j++)
                {
                    LocalizationKey? _key = default;
                    archive.Read(ref _key);
                    var Entry = KeyTable.FindOrAdd(_key);

                    int hash = 0;
                    archive.Read(ref hash);
                    Entry.SourceStringHash = hash;
                    if (IsCompact)
                    {
                        int localizedStringIndex = 0;
                        archive.Read(ref localizedStringIndex);
                        if (localizedStringIndex > -1 && localizedStringIndex < LocalizedStringArray.Count)
                        {
                            Entry.LocalizedString = LocalizedStringArray[localizedStringIndex].Value;
                        }
                    }
                    else
                    {
                        FString? localizedString = default;
                        archive.Read(ref localizedString);
                        Entry.LocalizedString = localizedString;
                    }
                }
            }
            return archive;
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
