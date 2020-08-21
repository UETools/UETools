using System;
using System.Runtime.InteropServices;
using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Vector : IUnrealStruct, IEquatable<Vector>
    {
        public FArchive Serialize(FArchive reader) 
            => reader.Read(ref _x)
                     .Read(ref _y)
                     .Read(ref _z);

        public override string ToString() => $"{{ X: {_x}, Y: {_y}, Z: {_z} }}";
        public override bool Equals(object? obj) => obj switch
        {
            Vector vec => Equals(vec),
            _ => false
        };
        public bool Equals(Vector other) => other._x == _x && other._y == _y && other._z == _z;
        public override int GetHashCode() => HashCode.Combine(_x, _y, _z);
        public static bool operator ==(Vector left, Vector right) => left.Equals(right);
        public static bool operator !=(Vector left, Vector right) => !(left == right);

        [FieldOffset(0)]
        private float _x;
        [FieldOffset(4)]
        private float _y;
        [FieldOffset(8)]
        private float _z;
    }
}
