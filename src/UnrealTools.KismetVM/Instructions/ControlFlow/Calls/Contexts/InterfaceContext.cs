using System;
using System.IO;
using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class InterfaceContext : Token
    {
        public override EExprToken Expr => EExprToken.EX_InterfaceContext;

        public Token InterfaceExpression { get; private set; } = null!;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            InterfaceExpression = Token.Read(reader);
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
