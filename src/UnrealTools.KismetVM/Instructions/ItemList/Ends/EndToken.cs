using System.IO;
using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal abstract class EndToken : Token
    {
        public override void Deserialize(FArchive reader) => base.Deserialize(reader);

        public override void ReadTo(TextWriter writer) => writer.Write(']');
    }
}
