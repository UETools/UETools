using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct Matrix : IUnrealStruct
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _x);
            reader.Read(out _y);
            reader.Read(out _z);
            reader.Read(out _w);
        }
        public override string ToString() => $"{{ X: {_x}, Y: {_y}, Z: {_z}, W: {_w} }}";

        private Plane _x;
        private Plane _y;
        private Plane _z;
        private Plane _w;
    }
}
