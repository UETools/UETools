using System;
using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    public struct IntPoint : IUnrealStruct, IEquatable<IntPoint>
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _x);
            reader.Read(out _y);
        }

        public override string ToString() => $"{{ X: {_x}, Y: {_y} }}";
        public override bool Equals(object? obj) => obj switch
        {
            IntPoint vec => Equals(vec),
            _ => false
        };
        public bool Equals(IntPoint other) => other._x == _x && other._y == _y;
        public override int GetHashCode() => HashCode.Combine(_x, _y);
        public static bool operator ==(IntPoint left, IntPoint right) => left.Equals(right);
        public static bool operator !=(IntPoint left, IntPoint right) => !(left == right);

        private int _x;
        private int _y;
    }
}
