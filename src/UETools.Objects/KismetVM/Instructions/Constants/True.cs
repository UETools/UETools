using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class True : ConstToken<bool>
    {
        public override EExprToken Expr => EExprToken.EX_True;

        public override FArchive Serialize(FArchive reader)
        {
            _value = true;
            return base.Serialize(reader);
        }
    }
}
