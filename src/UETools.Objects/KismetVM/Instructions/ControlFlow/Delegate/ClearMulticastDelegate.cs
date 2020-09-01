using System;
using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    public sealed class ClearMulticastDelegate : Token
    {
        public override EExprToken Expr => EExprToken.EX_ClearMulticastDelegate;

        public Token FirstExpr { get; private set; } = null!;

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);
            FirstExpr = Token.Read(archive);
            return archive;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
