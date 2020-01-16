using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    public struct Vector4 : IUnrealStruct
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _x);
            reader.Read(out _y);
            reader.Read(out _z);
            reader.Read(out _w);
        }
        public override string ToString() => $"{{ X: {_x}, Y: {_y}, Z: {_z}, W: {_w} }}";

        private float _x;
        private float _y;
        private float _z;
        private float _w;
    }
}
