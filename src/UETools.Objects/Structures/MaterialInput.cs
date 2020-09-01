using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    class MaterialInput : IUnrealStruct
    {
        public virtual FArchive Serialize(FArchive archive)
            => archive.Read(ref _inputName)
                      .Read(ref _mask)
                      .Read(ref _maskR)
                      .Read(ref _maskG)
                      .Read(ref _maskB)
                      .Read(ref _maskA)
                      .Read(ref _expressionName);

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