using UETools.Core;

namespace UETools.Objects.Structures
{
    class ExpressionInput : MaterialInput
    {
        public override void Deserialize(FArchive reader)
        {
            reader.Read(out _outputIndex);
            base.Deserialize(reader);
        }

        private int _outputIndex;
    }
}
