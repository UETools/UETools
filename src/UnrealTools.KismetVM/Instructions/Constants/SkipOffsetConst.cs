using System;
using System.IO;
using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class SkipOffsetConst : Token
    {
        public override EExprToken Expr => EExprToken.EX_SkipOffsetConst;

        public CodeSkipSize Skip => _skip;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _skip);
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private CodeSkipSize _skip;
    }
}
