namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class LetBool : LetToken
    {
        public override EExprToken Expr => EExprToken.EX_LetBool;
    }
}
