using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal abstract class EndToken : Token
    {
        public override void Deserialize(FArchive reader) => base.Deserialize(reader);

        public override void ReadTo(TextWriter writer) => writer.Write(']');
    }
}
