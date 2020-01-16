using System;
using System.IO;
using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    public sealed class ClearMulticastDelegate : Token
    {
        public override EExprToken Expr => EExprToken.EX_ClearMulticastDelegate;

        public Token FirstExpr { get; private set; } = null!;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            FirstExpr = Token.Read(reader);
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
