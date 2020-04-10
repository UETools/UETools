using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class Skip : Token
    {
        public override EExprToken Expr => EExprToken.EX_Skip;

        public CodeSkipSize Size { get => _size; set => _size = value; }
        public Token SkipExpression { get; private set; } = null!;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _size);
            SkipExpression = Token.Read(reader);
        }

        public override void ReadTo(TextWriter writer)
        {
            writer.Write($"Skip {Size} bytes of ");
            if (SkipExpression != null)
                SkipExpression.ReadTo(writer);
        }

        private CodeSkipSize _size;
    }
}
