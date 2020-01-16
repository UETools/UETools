using System;
using System.Collections.Generic;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Core.Collections
{
    class TMap<TKey, TValue> : Dictionary<TKey, TValue>, IUnrealDeserializable where TKey : notnull
    {
        public void Deserialize(FArchive reader)
        {
            throw new NotImplementedException();
        }
    }
}
