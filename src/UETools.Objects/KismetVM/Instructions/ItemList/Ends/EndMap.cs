﻿namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class EndMap : EndToken
    {
        public override EExprToken Expr => EExprToken.EX_EndMap;
    }
}
