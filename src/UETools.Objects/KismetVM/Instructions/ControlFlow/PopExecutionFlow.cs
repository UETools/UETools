using System;
using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class PopExecutionFlow : Token
    {
        public override EExprToken Expr => EExprToken.EX_PopExecutionFlow;

        public override FArchive Serialize(FArchive reader) => base.Serialize(reader);

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
