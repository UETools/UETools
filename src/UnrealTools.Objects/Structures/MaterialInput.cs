using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    class MaterialInput : IUnrealStruct
    {
        public virtual void Deserialize(FArchive reader)
        {
            reader.Read(out _inputName);
            reader.Read(out _mask);
            reader.Read(out _maskR);
            reader.Read(out _maskG);
            reader.Read(out _maskB);
            reader.Read(out _maskA);
            reader.Read(out _expressionName);
        }

        public override string ToString() => $"{_expressionName}: {_inputName}";

        private FName _inputName = null!;
        private int _mask;
        private int _maskR;
        private int _maskG;
        private int _maskB;
        private int _maskA;
        private FName _expressionName = null!;
    }
}