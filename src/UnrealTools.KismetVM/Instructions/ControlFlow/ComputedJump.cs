using System.IO;
using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class ComputedJump : Token
    {
        public override EExprToken Expr => EExprToken.EX_ComputedJump;

        public Token Offset { get; private set; } = null!;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            Offset = Token.Read(reader);
        }

        public override void ReadTo(TextWriter writer)
        {
            writer.Write("jump ");
            Offset.ReadTo(writer);
        }
    }
}
