using System.IO;
using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class ArrayGetByRef : Token
    {
        public override EExprToken Expr => EExprToken.EX_ArrayGetByRef;

        public Token ArrayExpr { get; private set; } = null!;
        public Token IndexExpr { get; private set; } = null!;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            ArrayExpr = Token.Read(reader);
            IndexExpr = Token.Read(reader);
        }

        public override void ReadTo(TextWriter writer)
        {
            ArrayExpr.ReadTo(writer);
            writer.Write('[');
            IndexExpr.ReadTo(writer);
            writer.Write(']');
        }
    }
}
