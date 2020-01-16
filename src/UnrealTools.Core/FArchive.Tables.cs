using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Core
{
    public partial class FArchive
    {
        /// <summary>
        /// Gets instance of <see cref="UnrealTable{T}"/> that has been added with name <paramref name="tableName"/> and has entries of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of keys the found instance stores.</typeparam>
        /// <param name="tableName">Name of instance in the tables dictionary.</param>
        /// <returns>Instance of the type deriving from <see cref="UnrealTable{T}"/> with specified name, or null.</returns>
        public UnrealTable<T>? GetTable<T>(string tableName) where T : notnull
            => Tables.TryGetValue(tableName, out var table) ? table as UnrealTable<T> : default;

        /// <summary>
        /// Searches for first table that stores entries of type <typeparamref name="T"/>, and returns its instance on success.
        /// </summary>
        /// <typeparam name="T">Type of keys the found instance stores.</typeparam>
        /// <param name="table">Found instance</param>
        /// <returns><see langword="true"/> if instance has been found; otherwise <see langword="false"/>.</returns>
        public bool FindTableOfType<T>([NotNullWhen(true)] out UnrealTable<T>? table) where T : notnull
        {
            var kv = Tables.FirstOrDefault(t => t.Value is UnrealTable<T>);
            table = kv.Value as UnrealTable<T>;
            return table != null;
        }

        internal ConcurrentDictionary<string, IUnrealTable> Tables { get; private set; } = new ConcurrentDictionary<string, IUnrealTable>();
        public IUnrealLocalizationProvider? Localization { get; set; }
    }
}
