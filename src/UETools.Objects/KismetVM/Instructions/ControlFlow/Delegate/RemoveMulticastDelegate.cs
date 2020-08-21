using System;
using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class RemoveMulticastDelegate : Token
    {
        public override EExprToken Expr => EExprToken.EX_RemoveMulticastDelegate;

        public Token FirstExpr { get; private set; } = null!;
        public Token SecondExpr { get; private set; } = null!;

        public override FArchive Serialize(FArchive reader)
        {
            base.Serialize(reader);
            FirstExpr = Token.Read(reader);
            SecondExpr = Token.Read(reader);
            return reader;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
