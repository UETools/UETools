using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    partial struct RichCurveKey : IUnrealStruct
    {
        public FArchive Serialize(FArchive reader) 
            => reader.ReadUnsafe(ref _interpMode)
                     .ReadUnsafe(ref _tangentMode)
                     .ReadUnsafe(ref _tangentWeightMode)
                     .Read(ref _time)
                     .Read(ref _value)
                     .Read(ref _arriveTangent)
                     .Read(ref _arriveTangentWeight)
                     .Read(ref _leaveTangent)
                     .Read(ref _leaveTangentWeight);

        public override string ToString() => $"{{ Time: {_time}, Value: {_value}, InterpMode: {_interpMode} }}";

        private InterpMode _interpMode;
        private TangentMode _tangentMode;
        private TangentWeightMode _tangentWeightMode;
        private float _time;
        private float _value;
        private float _arriveTangent;
        private float _arriveTangentWeight;
        private float _leaveTangent;
        private float _leaveTangentWeight;
    }
}
