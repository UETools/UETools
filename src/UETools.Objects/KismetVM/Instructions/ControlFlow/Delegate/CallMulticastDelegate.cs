using System;
using System.IO;
using UETools.Core;
using UETools.Objects.Package;

namespace UETools.Objects.KismetVM.Instructions
{
    public sealed class CallMulticastDelegate : Token
    {
        public override EExprToken Expr => EExprToken.EX_CallMulticastDelegate;

        public ObjectReference CallTo { get => _callTo; set => _callTo = value; }
        public Token FirstExpr { get; set; } = null!;
        public TokenList Params { get; } = new TokenList();

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive)
                .Read(ref _callTo);
            FirstExpr = Token.Read(archive);
            Params.ReadUntil(archive, EExprToken.EX_EndFunctionParms);
            return archive;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private ObjectReference _callTo = null!;
    }
}
