using System;
using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class PrimitiveCast : Token
    {
        public override EExprToken Expr => EExprToken.EX_PrimitiveCast;
        public ECastToken CastTo { get => _castTo; set => _castTo = value; }
        public Token CastExpression { get; private set; } = null!;

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive)
                .ReadUnsafe(ref _castTo);
            CastExpression = Token.Read(archive);
            return archive;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private ECastToken _castTo;
    }
}
