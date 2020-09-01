using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class IntOne : ConstToken<int>
    {
        public override EExprToken Expr => EExprToken.EX_IntOne;

        public override FArchive Serialize(FArchive archive)
        {
            _value = 1;
            return base.Serialize(archive);
        }
    }
}
