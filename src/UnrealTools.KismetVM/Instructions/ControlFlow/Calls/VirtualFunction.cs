using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class VirtualFunction : CallToken<FName>
    {
        public override EExprToken Expr => EExprToken.EX_VirtualFunction;
    }
}
