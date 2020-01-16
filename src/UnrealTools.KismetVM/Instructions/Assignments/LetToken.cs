using System.IO;
using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal abstract class LetToken : Token
    {
        public Token To { get; private set; } = null!;
        public Token From { get; private set; } = null!;
        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            To = Token.Read(reader);
            From = Token.Read(reader);
        }

        public override void ReadTo(TextWriter writer)
        {
            To.ReadTo(writer);
            writer.Write(" = ");
            From.ReadTo(writer);
        }
    }
}
