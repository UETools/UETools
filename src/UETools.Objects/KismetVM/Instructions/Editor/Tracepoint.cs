using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class Tracepoint : Token
    {
        public override EExprToken Expr => EExprToken.EX_Tracepoint;

        public override FArchive Serialize(FArchive archive) => base.Serialize(archive);

        public override void ReadTo(TextWriter writer) => writer.WriteLine("Tracepoint");
    }
}
