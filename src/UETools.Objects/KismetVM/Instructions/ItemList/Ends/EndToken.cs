using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal abstract class EndToken : Token
    {
        public override FArchive Serialize(FArchive reader) => base.Serialize(reader);

        public override void ReadTo(TextWriter writer) => writer.Write(']');
    }
}
