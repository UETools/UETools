using UnrealTools.Objects.Package;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class LocalFinalFunction : CallToken<ObjectReference>
    {
        public override EExprToken Expr => EExprToken.EX_LocalFinalFunction;
    }
}
