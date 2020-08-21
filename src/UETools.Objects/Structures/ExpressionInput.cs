using UETools.Core;

namespace UETools.Objects.Structures
{
    class ExpressionInput : MaterialInput
    {
        public override FArchive Serialize(FArchive reader) => base.Serialize(reader.Read(ref _outputIndex));

        private int _outputIndex;
    }
}
