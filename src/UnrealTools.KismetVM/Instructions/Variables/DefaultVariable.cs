namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class DefaultVariable : VariableToken
    {
        public override EExprToken Expr => EExprToken.EX_DefaultVariable;
    }
}
