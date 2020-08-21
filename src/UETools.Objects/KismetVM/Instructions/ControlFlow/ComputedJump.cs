using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class ComputedJump : Token
    {
        public override EExprToken Expr => EExprToken.EX_ComputedJump;

        public Token Offset { get; private set; } = null!;

        public override FArchive Serialize(FArchive reader)
        {
            base.Serialize(reader);
            Offset = Token.Read(reader);
            return reader;
        }

        public override void ReadTo(TextWriter writer)
        {
            writer.Write("jump ");
            Offset.ReadTo(writer);
        }
    }
}
