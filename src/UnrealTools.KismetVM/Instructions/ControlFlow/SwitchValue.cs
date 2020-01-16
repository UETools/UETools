using System;
using System.IO;
using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class SwitchValue : Token
    {
        public override EExprToken Expr => EExprToken.EX_SwitchValue;
        public ushort NumCases { get => _numCases; set => _numCases = value; }
        public CodeSkipSize AfterSkip { get => _afterSkip; set => _afterSkip = value; }
        public Token IndexExpression { get; private set; } = null!;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _numCases);
            reader.Read(out _afterSkip);
            IndexExpression = Token.Read(reader);
            for (int i = 0; i < NumCases; i++)
            {
                var label = Token.Read(reader);
                reader.Read(out CodeSkipSize _nextCaseOffset);
                var term = Token.Read(reader);
            }
            var defaultCase = Token.Read(reader);
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private ushort _numCases;
        private CodeSkipSize _afterSkip;
    }
}
