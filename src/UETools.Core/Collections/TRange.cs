using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core.Interfaces;

namespace UETools.Core.Collections
{
    public struct TRange<T> : IUnrealSerializable where T : IUnrealSerializable?, new()
    {
        public FArchive Serialize(FArchive reader)
            => reader.Read(ref _lowerBound)
                     .Read(ref _upperBound);

        TRangeBound _lowerBound;
        TRangeBound _upperBound;

        enum ERangeBoundTypes : byte
        {
            Exclusive,
            Inclusive,
            Open,
        }

        struct TRangeBound : IUnrealSerializable
        {
            public FArchive Serialize(FArchive reader) 
                => reader.ReadUnsafe(ref _type)
                         .Read(ref _value);

            ERangeBoundTypes _type;
            T _value;
        }
    }
}
