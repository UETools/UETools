namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class ObjToInterfaceCast : CastToken
    {
        public override EExprToken Expr => EExprToken.EX_ObjToInterfaceCast;
    }
}
