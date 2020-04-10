using UETools.Objects.Package;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class LocalFinalFunction : CallToken<ObjectReference>
    {
        public override EExprToken Expr => EExprToken.EX_LocalFinalFunction;
    }
}
