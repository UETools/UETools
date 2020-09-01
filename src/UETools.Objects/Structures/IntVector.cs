using System;
using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct IntVector : IUnrealStruct, IEquatable<IntVector>
    {
        public FArchive Serialize(FArchive archive)
            => archive.Read(ref _x)
                      .Read(ref _y)
                      .Read(ref _z);

        public override string ToString() => $"{{ X: {_x}, Y: {_y}, Z: {_z} }}";
        public override bool Equals(object? obj) => obj switch
        {
            IntVector vec => Equals(vec),
            _ => false
        };
        public bool Equals(IntVector other) => other._x == _x && other._y == _y && other._z == _z;
        public override int GetHashCode() => HashCode.Combine(_x, _y, _z);
        public static bool operator ==(IntVector left, IntVector right) => left.Equals(right);
        public static bool operator !=(IntVector left, IntVector right) => !(left == right);

        private int _x;
        private int _y;
        private int _z;
    }
}
