﻿namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class EndSet : EndToken
    {
        public override EExprToken Expr => EExprToken.EX_EndSet;
    }
}
