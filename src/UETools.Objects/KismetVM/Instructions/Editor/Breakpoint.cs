using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class Breakpoint : Token
    {
        public override EExprToken Expr => EExprToken.EX_Breakpoint;

        public override FArchive Serialize(FArchive archive) => base.Serialize(archive);

        public override void ReadTo(TextWriter writer) => writer.WriteLine("Breakpoint");
    }
}
