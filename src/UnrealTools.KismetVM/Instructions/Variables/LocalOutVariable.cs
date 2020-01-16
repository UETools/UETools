namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class LocalOutVariable : VariableToken
    {
        public override EExprToken Expr => EExprToken.EX_LocalOutVariable;
    }
}
