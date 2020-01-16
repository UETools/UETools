using System;
using System.IO;
using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class PopExecutionFlowIfNot : Token
    {
        public override EExprToken Expr => EExprToken.EX_PopExecutionFlowIfNot;

        public Token NotExpression { get; private set; } = null!;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            NotExpression = Token.Read(reader);
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
