using System.IO;
using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class PushExecutionFlow : Token
    {
        public override EExprToken Expr => EExprToken.EX_PushExecutionFlow;

        public CodeSkipSize Size { get => _size; set => _size = value; }

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _size);
        }

        public override void ReadTo(TextWriter writer) => writer.WriteLine($"push flowstack {Size}");

        private CodeSkipSize _size;
    }
}
