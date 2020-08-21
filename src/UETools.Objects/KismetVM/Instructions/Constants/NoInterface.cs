using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class NoInterface : ConstToken<object?>
    {
        public override EExprToken Expr => EExprToken.EX_NoInterface;

        public override FArchive Serialize(FArchive reader)
        {
            _value = null;
            return base.Serialize(reader);
        }
    }
}
