using System.IO;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class EndOfScript : Token
    {
        public override EExprToken Expr => EExprToken.EX_EndOfScript;

        public override void ReadTo(TextWriter writer) => writer.WriteLine("EOF"); 
    }
}
