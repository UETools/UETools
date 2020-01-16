namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class EndSetConst : EndToken
    {
        public override EExprToken Expr => EExprToken.EX_EndSetConst;
    }
}
