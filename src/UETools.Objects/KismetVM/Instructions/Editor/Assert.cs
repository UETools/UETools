using System;
using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class Assert : Token
    {
        public override EExprToken Expr => EExprToken.EX_Assert;

        public ushort LineNumber { get => _lineNumber; set => _lineNumber = value; }
        public byte InDebugMode { get => _inDebugMode; set => _inDebugMode = value; }
        public Token Test { get; private set; } = null!;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _lineNumber);
            reader.Read(out _inDebugMode);
            Test = Token.Read(reader);
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private ushort _lineNumber;
        private byte _inDebugMode;
    }
}
