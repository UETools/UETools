using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class False : ConstToken<bool>
    {
        public override EExprToken Expr => EExprToken.EX_False;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            _value = false;
        }
    }
}
