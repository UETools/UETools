using UETools.Core;
using UETools.Objects.Structures;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class VectorConst : ConstToken<Vector>
    {
        public override EExprToken Expr => EExprToken.EX_VectorConst;

        public override FArchive Serialize(FArchive archive)
            => base.Serialize(archive)
                   .Read(ref _value);
    }
}
