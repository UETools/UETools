using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct LinearColor : IUnrealStruct
    {
        public FArchive Serialize(FArchive archive)
            => archive.Read(ref _b)
                      .Read(ref _g)
                      .Read(ref _r)
                      .Read(ref _a);
        public override string ToString() => $"{{ R: {_r}, G: {_g}, B: {_b}, A: {_a} }}";

        private float _b;
        private float _g;
        private float _r;
        private float _a;
    }
}
