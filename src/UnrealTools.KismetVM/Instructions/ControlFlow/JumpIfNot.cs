using System.IO;
using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class JumpIfNot : Token
    {
        public override EExprToken Expr => EExprToken.EX_JumpIfNot;

        public CodeSkipSize Size => _size;
        public Token Expression { get; private set; } = null!;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _size);
            Expression = Token.Read(reader);
        }

        public override void ReadTo(TextWriter writer)
        {
            writer.Write("if not ");
            if (Expression != null)
                Expression.ReadTo(writer);
            writer.Write("jump to {0}", Size);
        }

        private CodeSkipSize _size;
    }
}
