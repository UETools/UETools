using System;
using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class SetMap : CollectionToken
    {
        public override EExprToken Expr => EExprToken.EX_SetMap;

        public Token MapToken { get; private set; } = null!;
        public int Count => _count;
        public TokenList Items { get; } = new TokenList();

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            MapToken = Token.Read(reader);
            reader.Read(out _count);
            Items.ReadUntil(reader, EExprToken.EX_EndMap);
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private int _count;
    }
}
