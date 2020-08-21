using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class NoObject : ConstToken<object?>
    {
        public override EExprToken Expr => EExprToken.EX_NoObject;

        public override FArchive Serialize(FArchive reader)
        {
            _value = null;
            return base.Serialize(reader);
        }
    }
}
