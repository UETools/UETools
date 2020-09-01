using System;
using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct IntPoint : IUnrealStruct, IEquatable<IntPoint>
    {
        public FArchive Serialize(FArchive archive)
            => archive.Read(ref _x)
                      .Read(ref _y);

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
