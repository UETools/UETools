namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class LocalVariable : VariableToken
    {
        public override EExprToken Expr => EExprToken.EX_LocalVariable;
    }
}
