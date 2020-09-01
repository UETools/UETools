using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class NoInterface : ConstToken<object?>
    {
        public override EExprToken Expr => EExprToken.EX_NoInterface;

        public override FArchive Serialize(FArchive archive) => base.Serialize(archive);
    }
}
