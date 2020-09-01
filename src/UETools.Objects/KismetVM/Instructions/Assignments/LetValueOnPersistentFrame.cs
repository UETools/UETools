using System;
using System.IO;
using UETools.Core;
using UETools.Objects.Package;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class LetValueOnPersistentFrame : Token
    {
        public override EExprToken Expr => EExprToken.EX_LetValueOnPersistentFrame;

        public Token Expression { get; private set; } = null!;

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive)
                .Read(ref _prop);
            Expression = Token.Read(archive);
            return archive;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private ObjectReference _prop = null!;
    }
}
