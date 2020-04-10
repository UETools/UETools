namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class InterfaceToObjCast : CastToken
    {
        public override EExprToken Expr => EExprToken.EX_InterfaceToObjCast;
    }
}
