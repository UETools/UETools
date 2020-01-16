using System;
using System.Collections.Generic;
using System.Text;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Core.Collections
{
    public struct TRange<T> : IUnrealDeserializable where T : IUnrealDeserializable?, new()
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _lowerBound);
            reader.Read(out _upperBound);
        }

        TRangeBound _lowerBound;
        TRangeBound _upperBound;

        enum ERangeBoundTypes : byte
        {
            Exclusive,
            Inclusive,
            Open,
        }
        struct TRangeBound : IUnrealDeserializable
        {
            public void Deserialize(FArchive reader)
            {
                reader.ReadUnsafe(out _type);
                reader.Read(out _value);
            }

            ERangeBoundTypes _type;
            T _value;
        }
    }
}
