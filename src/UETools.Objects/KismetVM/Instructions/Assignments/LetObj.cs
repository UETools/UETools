namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class LetObj : LetToken
    {
        public override EExprToken Expr => EExprToken.EX_LetObj;
    }
}
