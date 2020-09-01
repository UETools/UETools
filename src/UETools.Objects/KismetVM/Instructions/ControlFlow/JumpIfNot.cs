using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class JumpIfNot : Token
    {
        public override EExprToken Expr => EExprToken.EX_JumpIfNot;

        public CodeSkipSize Size => _size;
        public Token Expression { get; private set; } = null!;

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive)
                .Read(ref _size);
            Expression = Token.Read(archive);
            return archive;
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
