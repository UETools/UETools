using System;
using System.Collections.Generic;
using UETools.Core.Interfaces;

namespace UETools.Core.Collections
{
    class TArray<T> : List<T>, IUnrealDeserializable
    {
        public void Deserialize(FArchive reader)
        {
            throw new NotImplementedException();
        }
    }
}
