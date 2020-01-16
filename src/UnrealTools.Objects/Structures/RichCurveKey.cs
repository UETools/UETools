using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    partial struct RichCurveKey : IUnrealStruct
    {
        public void Deserialize(FArchive reader)
        {
            reader.ReadUnsafe(out _interpMode);
            reader.ReadUnsafe(out _tangentMode);
            reader.ReadUnsafe(out _tangentWeightMode);
            reader.Read(out _time);
            reader.Read(out _value);
            reader.Read(out _arriveTangent);
            reader.Read(out _arriveTangentWeight);
            reader.Read(out _leaveTangent);
            reader.Read(out _leaveTangentWeight);
        }

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
