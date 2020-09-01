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

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);
            ObjectExpression = Token.Read(archive);
            archive.Read(ref _skipCount)
                   .Read(ref _field);
            ContextExpression = Token.Read(archive);
            return archive;
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
