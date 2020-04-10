using System;
using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class SetSet : ListToken
    {
        public override EExprToken Expr => EExprToken.EX_SetSet;

        public Token SetToken { get; private set; } = null!;
        public int Count => _count;
        public TokenList Items { get; } = new TokenList();

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            SetToken = Token.Read(reader);
            reader.Read(out _count);
            Items.ReadUntil(reader, EExprToken.EX_EndSet);
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private int _count;
    }
}
