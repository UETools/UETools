using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal abstract class EndToken : Token
    {
        public override FArchive Serialize(FArchive archive) => base.Serialize(archive);

        public override void ReadTo(TextWriter writer) => writer.Write(']');
    }
}
