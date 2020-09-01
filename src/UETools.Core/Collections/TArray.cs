using System;
using System.Collections.Generic;
using UETools.Core.Interfaces;

namespace UETools.Core.Collections
{
    class TArray<T> : List<T>, IUnrealSerializable
    {
        public FArchive Serialize(FArchive archive)
        {
            throw new NotImplementedException();
        }
    }
}
