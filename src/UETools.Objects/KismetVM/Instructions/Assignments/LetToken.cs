using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal abstract class LetToken : Token
    {
        public Token To { get; private set; } = null!;
        public Token From { get; private set; } = null!;
        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);
            To = Token.Read(archive);
            From = Token.Read(archive);
            return archive;
        }

        public override void ReadTo(TextWriter writer)
        {
            To.ReadTo(writer);
            writer.Write(" = ");
            From.ReadTo(writer);
        }
    }
}
