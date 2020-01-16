using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class LocalVirtualFunction : CallToken<FName>
    {
        public override EExprToken Expr => EExprToken.EX_LocalVirtualFunction;
    }
}
