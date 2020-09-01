using System;
using System.IO;
using UETools.Core;
using UETools.Objects.Package;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class StructMemberContext : Token
    {
        public override EExprToken Expr => EExprToken.EX_StructMemberContext;

        public ObjectReference Property => _property;
        public Token StructExpression { get; private set; } = null!;

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive)
                .Read(ref _property);
            StructExpression = Token.Read(archive);
            return archive;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private ObjectReference _property = null!;
    }
}
