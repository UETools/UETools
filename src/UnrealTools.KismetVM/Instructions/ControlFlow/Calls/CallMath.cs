using UnrealTools.Objects.Package;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class CallMath : CallToken<ObjectReference>
    {
        public override EExprToken Expr => EExprToken.EX_CallMath;
    }
}
