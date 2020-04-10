using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class Jump : Token
    {
        public override EExprToken Expr => EExprToken.EX_Jump;

        public CodeSkipSize SkipCount { get => _skipCount; set => _skipCount = value; }

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _skipCount);
        }

        public override void ReadTo(TextWriter writer) => writer.Write("Jump to {0}", SkipCount);

        private CodeSkipSize _skipCount;
    }
}
