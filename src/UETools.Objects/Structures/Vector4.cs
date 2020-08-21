using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct Vector4 : IUnrealStruct
    {
        public FArchive Serialize(FArchive reader)
            => reader.Read(ref _x)
                     .Read(ref _y)
                     .Read(ref _z)
                     .Read(ref _w);
        public override string ToString() => $"{{ X: {_x}, Y: {_y}, Z: {_z}, W: {_w} }}";

        private float _x;
        private float _y;
        private float _z;
        private float _w;
    }
}
