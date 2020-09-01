using System;
using System.Runtime.InteropServices;
using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Vector2D : IUnrealStruct, IEquatable<Vector2D>
    {
        public FArchive Serialize(FArchive archive)
            => archive.Read(ref _x)
                      .Read(ref _y);

        public override string ToString() => $"{{ X: {_x}, Y: {_y} }}";
        public override bool Equals(object? obj) => obj switch
        {
            Vector2D vec => Equals(vec),
            _ => false
        };
        public bool Equals(Vector2D other) => other._x == _x && other._y == _y;
        public override int GetHashCode() => HashCode.Combine(_x, _y);
        public static bool operator ==(Vector2D left, Vector2D right) => left.Equals(right);
        public static bool operator !=(Vector2D left, Vector2D right) => !(left == right);

        [FieldOffset(0)]
        private float _x;
        [FieldOffset(4)]
        private float _y;
    }
}
