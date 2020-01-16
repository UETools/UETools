using System.IO;
using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class Breakpoint : Token
    {
        public override EExprToken Expr => EExprToken.EX_Breakpoint;

        public override void Deserialize(FArchive reader) => base.Deserialize(reader);

        public override void ReadTo(TextWriter writer) => writer.WriteLine("Breakpoint");
    }
}
