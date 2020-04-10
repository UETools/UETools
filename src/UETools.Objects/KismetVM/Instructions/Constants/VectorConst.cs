using UETools.Core;
using UETools.Objects.Structures;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class VectorConst : ConstToken<Vector>
    {
        public override EExprToken Expr => EExprToken.EX_VectorConst;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _value);
        }
    }
}
