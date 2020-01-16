namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class LetObj : LetToken
    {
        public override EExprToken Expr => EExprToken.EX_LetObj;
    }
}
