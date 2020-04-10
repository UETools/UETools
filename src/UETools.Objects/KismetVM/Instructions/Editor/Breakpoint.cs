using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class Breakpoint : Token
    {
        public override EExprToken Expr => EExprToken.EX_Breakpoint;

        public override void Deserialize(FArchive reader) => base.Deserialize(reader);

        public override void ReadTo(TextWriter writer) => writer.WriteLine("Breakpoint");
    }
}
