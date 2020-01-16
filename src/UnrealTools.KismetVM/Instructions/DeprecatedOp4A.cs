using System.IO;
using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class DeprecatedOp4A : Token
    {
        public override EExprToken Expr => EExprToken.EX_DeprecatedOp4A;

        public override void Deserialize(FArchive reader) => base.Deserialize(reader);

        public override void ReadTo(TextWriter writer) => writer.WriteLine(Expr);
    }
}
