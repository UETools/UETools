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

        public override FArchive Serialize(FArchive reader)
        {
            base.Serialize(reader)
                .Read(ref _callTo);
            FirstExpr = Token.Read(reader);
            Params.ReadUntil(reader, EExprToken.EX_EndFunctionParms);
            return reader;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private ObjectReference _callTo = null!;
    }
}
