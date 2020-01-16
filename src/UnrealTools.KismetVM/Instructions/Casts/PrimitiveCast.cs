using System;
using System.IO;
using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class PrimitiveCast : Token
    {
        public override EExprToken Expr => EExprToken.EX_PrimitiveCast;
        public ECastToken CastTo { get => _castTo; set => _castTo = value; }
        public Token CastExpression { get; private set; } = null!;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.ReadUnsafe(out _castTo);
            CastExpression = Token.Read(reader);
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private ECastToken _castTo;
    }
}
