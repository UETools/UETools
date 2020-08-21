using System;
using System.Collections.Generic;
using UETools.Core.Interfaces;

namespace UETools.Core.Collections
{
    class TMap<TKey, TValue> : Dictionary<TKey, TValue>, IUnrealSerializable where TKey : notnull
    {
        public FArchive Serialize(FArchive reader)
        {
            throw new NotImplementedException();
        }
    }
}
