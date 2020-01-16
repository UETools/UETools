namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class CrossInterfaceCast : CastToken
    {
        public override EExprToken Expr => EExprToken.EX_CrossInterfaceCast;
    }
}
