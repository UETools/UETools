namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class EndParmValue : EndToken
    {
        public override EExprToken Expr => EExprToken.EX_EndParmValue;
    }
}
