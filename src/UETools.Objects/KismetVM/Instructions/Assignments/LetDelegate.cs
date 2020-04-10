namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class LetDelegate : LetToken
    {
        public override EExprToken Expr => EExprToken.EX_LetDelegate;
    }
}
