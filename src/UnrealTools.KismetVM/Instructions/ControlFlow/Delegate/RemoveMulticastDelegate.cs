using System;
using System.IO;
using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class RemoveMulticastDelegate : Token
    {
        public override EExprToken Expr => EExprToken.EX_RemoveMulticastDelegate;

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
