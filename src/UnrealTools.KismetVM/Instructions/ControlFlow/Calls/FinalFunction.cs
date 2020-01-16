using UnrealTools.Objects.Package;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class FinalFunction : CallToken<ObjectReference>
    {
        public override EExprToken Expr => EExprToken.EX_FinalFunction;
    }
}
