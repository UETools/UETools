using System;
using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class SwitchValue : Token
    {
        public override EExprToken Expr => EExprToken.EX_SwitchValue;
        public ushort NumCases { get => _numCases; set => _numCases = value; }
        public CodeSkipSize AfterSkip { get => _afterSkip; set => _afterSkip = value; }
        public Token IndexExpression { get; private set; } = null!;

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive)
                .Read(ref _numCases)
                .Read(ref _afterSkip);
            IndexExpression = Token.Read(archive);
            for (int i = 0; i < NumCases; i++)
            {
                var label = Token.Read(archive);
                CodeSkipSize _nextCaseOffset = default;
                archive.Read(ref _nextCaseOffset);
                var term = Token.Read(archive);
            }
            var defaultCase = Token.Read(archive);
            return archive;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private ushort _numCases;
        private CodeSkipSize _afterSkip;
    }
}
