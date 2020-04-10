using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class WireTracepoint : Token
    {
        public override EExprToken Expr => EExprToken.EX_WireTracepoint;

        public override void Deserialize(FArchive reader) => base.Deserialize(reader);

        public override void ReadTo(TextWriter writer) => writer.WriteLine("WireTracepoint");
    }
}
