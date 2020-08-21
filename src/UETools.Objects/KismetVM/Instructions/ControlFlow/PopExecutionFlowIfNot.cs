using System;
using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class PopExecutionFlowIfNot : Token
    {
        public override EExprToken Expr => EExprToken.EX_PopExecutionFlowIfNot;

        public Token NotExpression { get; private set; } = null!;

        public override FArchive Serialize(FArchive reader)
        {
            base.Serialize(reader);
            NotExpression = Token.Read(reader);
            return reader;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
