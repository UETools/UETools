using System.Runtime.InteropServices;
using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct Color : IUnrealStruct
    {
        public FArchive Serialize(FArchive reader) 
            => reader.Read(ref _b)
                     .Read(ref _g)
                     .Read(ref _r)
                     .Read(ref _a);
        public override string ToString() => $"{{ R: {_r}, G: {_g}, B: {_b}, A: {_a} }}";

        private byte _b;
        private byte _g;
        private byte _r;
        private byte _a;
    }
}
