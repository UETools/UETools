namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class EndArrayConst : EndToken
    {
        public override EExprToken Expr => EExprToken.EX_EndArrayConst;
    }
}
