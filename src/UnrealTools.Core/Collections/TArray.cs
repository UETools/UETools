using System;
using System.Collections.Generic;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Core.Collections
{
    class TArray<T> : List<T>, IUnrealDeserializable
    {
        public void Deserialize(FArchive reader)
        {
            throw new NotImplementedException();
        }
    }
}
