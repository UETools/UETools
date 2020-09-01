using System;
using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    public sealed class AddMulticastDelegate : Token
    {
        public override EExprToken Expr => EExprToken.EX_AddMulticastDelegate;

        public Token FirstExpr { get; private set; } = null!;
        public Token SecondExpr { get; private set; } = null!;

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);
            FirstExpr = Token.Read(archive);
            SecondExpr = Token.Read(archive);
            return archive;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
