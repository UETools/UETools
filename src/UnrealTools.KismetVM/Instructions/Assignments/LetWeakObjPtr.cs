namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class LetWeakObjPtr : LetToken
    {
        public override EExprToken Expr => EExprToken.EX_LetWeakObjPtr;
    }
}
