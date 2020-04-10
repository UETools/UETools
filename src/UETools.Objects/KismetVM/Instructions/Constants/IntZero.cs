using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class IntZero : ConstToken<int>
    {
        public override EExprToken Expr => EExprToken.EX_IntZero;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            _value = 0;
        }
    }
}
