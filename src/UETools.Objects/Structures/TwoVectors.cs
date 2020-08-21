using System;
using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct TwoVectors : IUnrealStruct, IEquatable<TwoVectors>
    {
        public FArchive Serialize(FArchive reader) 
            => reader.Read(ref _v1)
                     .Read(ref _v2);

        public override string ToString() => $"{{ V1: {_v1}, V2: {_v2} }}";
        public override bool Equals(object? obj) => obj switch
        {
            TwoVectors vec => Equals(vec),
            _ => false
        };
        public bool Equals(TwoVectors other) => other._v1 == _v1 && other._v2 == _v2;
        public override int GetHashCode() => HashCode.Combine(_v1, _v2);
        public static bool operator ==(TwoVectors left, TwoVectors right) => left.Equals(right);
        public static bool operator !=(TwoVectors left, TwoVectors right) => !(left == right);

        private Vector _v1;
        private Vector _v2;
    }
}
