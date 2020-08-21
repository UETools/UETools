using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class ArrayGetByRef : Token
    {
        public override EExprToken Expr => EExprToken.EX_ArrayGetByRef;

        public Token ArrayExpr { get; private set; } = null!;
        public Token IndexExpr { get; private set; } = null!;

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);
            ArrayExpr = Token.Read(archive);
            IndexExpr = Token.Read(archive);
            return archive;
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
