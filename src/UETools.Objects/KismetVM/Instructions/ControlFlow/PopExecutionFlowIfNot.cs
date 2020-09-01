using System;
using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class PopExecutionFlowIfNot : Token
    {
        public override EExprToken Expr => EExprToken.EX_PopExecutionFlowIfNot;

        public Token NotExpression { get; private set; } = null!;

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);
            NotExpression = Token.Read(archive);
            return archive;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
