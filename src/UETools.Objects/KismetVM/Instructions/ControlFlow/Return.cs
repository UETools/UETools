using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class Return : Token
    {
        public override EExprToken Expr => EExprToken.EX_Return;
        public Token RetVal { get; private set; } = null!;

        public override FArchive Serialize(FArchive reader)
        {
            base.Serialize(reader);
            RetVal = Token.Read(reader);
            return reader;
        }

        public override void ReadTo(TextWriter writer)
        {
            writer.Write("return ");
            if (RetVal != null)
                RetVal.ReadTo(writer);

            writer.WriteLine();
        }
    }
}
