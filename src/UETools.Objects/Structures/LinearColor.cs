using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct LinearColor : IUnrealStruct
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _b);
            reader.Read(out _g);
            reader.Read(out _r);
            reader.Read(out _a);
        }
        public override string ToString() => $"{{ R: {_r}, G: {_g}, B: {_b}, A: {_a} }}";

        private float _a;
        private float _r;
        private float _g;
        private float _b;
    }
}
