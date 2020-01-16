using System;
using System.IO;
using UnrealTools.Core;
using UnrealTools.Objects.Package;

namespace UnrealTools.KismetVM.Instructions
{
    public sealed class CallMulticastDelegate : Token
    {
        public override EExprToken Expr => EExprToken.EX_CallMulticastDelegate;

        public ObjectReference CallTo { get => _callTo; set => _callTo = value; }
        public Token FirstExpr { get; set; } = null!;
        public TokenList Params { get; } = new TokenList();

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _callTo);
            FirstExpr = Token.Read(reader);
            Params.ReadUntil(reader, EExprToken.EX_EndFunctionParms);
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private ObjectReference _callTo = null!;
    }
}
