using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class IntZero : ConstToken<int>
    {
        public override EExprToken Expr => EExprToken.EX_IntZero;

        public override FArchive Serialize(FArchive reader)
        {
            _value = 0;
            return base.Serialize(reader);
        }
    }
}
