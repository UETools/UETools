using System.Runtime.InteropServices;
using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Color : IUnrealStruct
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _b);
            reader.Read(out _g);
            reader.Read(out _r);
            reader.Read(out _a);
        }
        public override string ToString() => $"{{ R: {_r}, G: {_g}, B: {_b}, A: {_a} }}";

        [FieldOffset(0)]
        private byte _b;
        [FieldOffset(1)]
        private byte _g;
        [FieldOffset(2)]
        private byte _r;
        [FieldOffset(3)]
        private byte _a;
    }
}
