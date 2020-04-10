using System;
using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class SetArray : ListToken
    {
        public override EExprToken Expr => EExprToken.EX_SetArray;

        public Token ArrayToken { get; private set; } = null!;
        public TokenList Items { get; } = new TokenList();

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            ArrayToken = Token.Read(reader);
            Items.ReadUntil(reader, EExprToken.EX_EndArray);
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
