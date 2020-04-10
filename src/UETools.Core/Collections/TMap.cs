using System;
using System.Collections.Generic;
using UETools.Core.Interfaces;

namespace UETools.Core.Collections
{
    class TMap<TKey, TValue> : Dictionary<TKey, TValue>, IUnrealDeserializable where TKey : notnull
    {
        public void Deserialize(FArchive reader)
        {
            throw new NotImplementedException();
        }
    }
}
