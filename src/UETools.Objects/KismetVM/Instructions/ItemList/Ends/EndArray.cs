namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class EndArray : EndToken
    {
        public override EExprToken Expr => EExprToken.EX_EndArray;
    }
}
