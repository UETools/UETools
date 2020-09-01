using System.IO;
using UETools.Core;
using UETools.Objects.Package;

namespace UETools.Objects.KismetVM.Instructions
{
    internal abstract class CastToken : Token
    {
        public ObjectReference Variable { get => _variable; set => _variable = value; }
        public Token CastExpr { get; private set; } = null!;

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive)
                .Read(ref _variable);
            CastExpr = Token.Read(archive);
            return archive;
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
