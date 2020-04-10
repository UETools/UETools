using System.IO;
using UETools.Core;
using UETools.Objects.Package;

namespace UETools.Objects.KismetVM.Instructions
{
    internal abstract class ContextToken : Token
    {
        public Token ObjectExpression { get; private set; } = null!;
        public CodeSkipSize SkipCount { get => _skipCount; set => _skipCount = value; }
        public ObjectReference Field { get => _field; set => _field = value; }
        public Token ContextExpression { get; private set; } = null!;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            ObjectExpression = Token.Read(reader);
            reader.Read(out _skipCount);
            reader.Read(out _field);
            ContextExpression = Token.Read(reader);
        }
        public override void ReadTo(TextWriter writer)
        {
            writer.Write("Context call ");
            ContextExpression.ReadTo(writer);
            writer.Write(" ");
            ObjectExpression.ReadTo(writer);
        }

        private ObjectReference _field = null!;
        private CodeSkipSize _skipCount;
    }
}
