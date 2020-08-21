using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct Matrix : IUnrealStruct
    {
        public FArchive Serialize(FArchive reader) 
            => reader.Read(ref _x)
                     .Read(ref _y)
                     .Read(ref _z)
                     .Read(ref _w);
        public override string ToString() => $"{{ X: {_x}, Y: {_y}, Z: {_z}, W: {_w} }}";

        private Plane _x;
        private Plane _y;
        private Plane _z;
        private Plane _w;
    }
}
