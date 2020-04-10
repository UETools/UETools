namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class InstanceVariable : VariableToken
    {
        public override EExprToken Expr => EExprToken.EX_InstanceVariable;
    }
}
