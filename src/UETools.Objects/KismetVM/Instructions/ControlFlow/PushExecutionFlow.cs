using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class PushExecutionFlow : Token
    {
        public override EExprToken Expr => EExprToken.EX_PushExecutionFlow;

        public CodeSkipSize Size { get => _size; set => _size = value; }

        public override FArchive Serialize(FArchive archive)
            => base.Serialize(archive)
                   .Read(ref _size);

        public override void ReadTo(TextWriter writer) => writer.WriteLine($"push flowstack {Size}");

        private CodeSkipSize _size;
    }
}
