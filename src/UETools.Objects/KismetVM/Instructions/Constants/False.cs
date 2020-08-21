using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class False : ConstToken<bool>
    {
        public override EExprToken Expr => EExprToken.EX_False;

        public override FArchive Serialize(FArchive archive)
        {
            _value = false;
            return base.Serialize(archive);
        }
    }
}
