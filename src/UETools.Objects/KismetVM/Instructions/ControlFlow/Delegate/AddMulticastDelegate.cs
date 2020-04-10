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

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            FirstExpr = Token.Read(reader);
            SecondExpr = Token.Read(reader);
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
