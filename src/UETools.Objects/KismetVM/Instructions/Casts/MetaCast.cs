namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class MetaCast : CastToken
    {
        public override EExprToken Expr => EExprToken.EX_MetaCast;
    }
}
