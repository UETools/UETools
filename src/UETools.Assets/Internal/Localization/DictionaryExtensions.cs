using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UETools.TypeFactory;

namespace UETools.Assets.Internal.Localization
{
    internal static class DictionaryExtensions
    {
        [return: NotNull]
        internal static TValue FindOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            where TValue : class, new()
            where TKey : notnull
        {
            if (!dict.TryGetValue(key, out var val))
            {
                val = Factory<TValue>.CreateInstance();
                dict.Add(key, val);
            }
            return val;
        }
    }
}