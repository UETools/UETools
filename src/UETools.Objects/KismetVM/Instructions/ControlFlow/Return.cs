using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class Return : Token
    {
        public override EExprToken Expr => EExprToken.EX_Return;
        public Token RetVal { get; private set; } = null!;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            RetVal = Token.Read(reader);
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
