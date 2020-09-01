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

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive)
                .Read(ref _lineNumber)
                .Read(ref _inDebugMode);
            Test = Token.Read(archive);
            return archive;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private ushort _lineNumber;
        private byte _inDebugMode;
    }
}
