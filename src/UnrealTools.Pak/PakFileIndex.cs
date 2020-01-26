using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnrealTools.Core;
using UnrealTools.Pak.Interfaces;

namespace UnrealTools.Pak
{
    public class PakFileIndex<T> where T : IEntry
    {
        private ConcurrentDictionary<string, PakFileIndex<T>> _directories = new ConcurrentDictionary<string, PakFileIndex<T>>();
        private Dictionary<string, T> _files = new Dictionary<string, T>();
        private string _parentDir;

        private PakFileIndex() : this(string.Empty) { }
        private PakFileIndex(string parentDir) => _parentDir = parentDir;

        public void Add(string path, T entry)
        {
            var parts = path.Split(new[] {
                Path.DirectorySeparatorChar,
                Path.AltDirectorySeparatorChar
            }, StringSplitOptions.RemoveEmptyEntries)
                .AsEnumerable()
                .GetEnumerator();
            if (parts.MoveNext())
                Add(parts, entry);
            else
                throw new UnrealException("Invalid path.");
        }

        void Add(IEnumerator<string> parts, T entry)
        {
            var first = parts.Current;
            if (parts.MoveNext())
            {
                var dir = _directories.GetOrAdd(first, new PakFileIndex<T>(first));
                dir.Add(parts, entry);
            }
            else
                _files.Add(first, entry);
        }

        public static PakFileIndex<T> Parse(Dictionary<string, T> entries)
        {
            var index = new PakFileIndex<T>();

            foreach (var kv in entries)
                index.Add(kv.Key, kv.Value);

            return index;
        }
    }
}
