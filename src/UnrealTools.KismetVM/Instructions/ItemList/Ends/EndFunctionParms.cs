namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class EndFunctionParms : EndToken
    {
        public override EExprToken Expr => EExprToken.EX_EndFunctionParms;
    }
}
