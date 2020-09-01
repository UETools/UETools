using UETools.Core;

namespace UETools.Objects.Structures
{
    class ExpressionInput : MaterialInput
    {
        public override FArchive Serialize(FArchive archive) => base.Serialize(archive.Read(ref _outputIndex));

        private int _outputIndex;
    }
}
