using System;
using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class SetArray : CollectionToken
    {
        public override EExprToken Expr => EExprToken.EX_SetArray;

        public Token ArrayToken { get; private set; } = null!;
        public TokenList Items { get; } = new TokenList();

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);
            ArrayToken = Token.Read(archive);
            Items.ReadUntil(archive, EExprToken.EX_EndArray);
            return archive;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
