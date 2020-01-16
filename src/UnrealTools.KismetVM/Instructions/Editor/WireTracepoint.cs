using System.IO;
using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class WireTracepoint : Token
    {
        public override EExprToken Expr => EExprToken.EX_WireTracepoint;

        public override void Deserialize(FArchive reader) => base.Deserialize(reader);

        public override void ReadTo(TextWriter writer) => writer.WriteLine("WireTracepoint");
    }
}
