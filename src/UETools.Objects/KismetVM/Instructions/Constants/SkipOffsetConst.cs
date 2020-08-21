using System;
using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class SkipOffsetConst : Token
    {
        public override EExprToken Expr => EExprToken.EX_SkipOffsetConst;

        public CodeSkipSize Skip => _skip;

        public override FArchive Serialize(FArchive reader) 
            => base.Serialize(reader)
                   .Read(ref _skip);

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private CodeSkipSize _skip;
    }
}
