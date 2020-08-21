using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class Self : Token
    {
        public override EExprToken Expr => EExprToken.EX_Self;

        public override FArchive Serialize(FArchive archive) => base.Serialize(archive);

        public override void ReadTo(TextWriter writer) => writer.Write("this");
    }
}
