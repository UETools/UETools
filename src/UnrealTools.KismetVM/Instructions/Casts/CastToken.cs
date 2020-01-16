using System.IO;
using UnrealTools.Core;
using UnrealTools.Objects.Package;

namespace UnrealTools.KismetVM.Instructions
{
    internal abstract class CastToken : Token
    {
        public ObjectReference Variable { get => _variable; set => _variable = value; }
        public Token CastExpr { get; private set; } = null!;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _variable);
            CastExpr = Token.Read(reader);
        }

        public override void ReadTo(TextWriter writer)
        {
            CastExpr.ReadTo(writer);
            writer.Write($"var({Variable.Resource?.FullName})");
        }

        // TODO: Should be UClass ?
        private ObjectReference _variable = null!;
    }
}
