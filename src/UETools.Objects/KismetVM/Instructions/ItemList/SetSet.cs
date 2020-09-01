using System;
using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class SetSet : CollectionToken
    {
        public override EExprToken Expr => EExprToken.EX_SetSet;

        public Token SetToken { get; private set; } = null!;
        public int Count => _count;
        public TokenList Items { get; } = new TokenList();

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);
            SetToken = Token.Read(archive);
            archive.Read(ref _count);
            Items.ReadUntil(archive, EExprToken.EX_EndSet);
            return archive;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private int _count;
    }
}
