﻿namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class DynamicCast : CastToken
    {
        public override EExprToken Expr => EExprToken.EX_DynamicCast;
    }
}
