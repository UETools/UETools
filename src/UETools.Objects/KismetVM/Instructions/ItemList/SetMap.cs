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

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);
            MapToken = Token.Read(archive);
            archive.Read(ref _count);
            Items.ReadUntil(archive, EExprToken.EX_EndMap);
            return archive;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private int _count;
    }
}
