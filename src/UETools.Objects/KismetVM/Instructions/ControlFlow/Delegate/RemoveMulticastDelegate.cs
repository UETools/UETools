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
