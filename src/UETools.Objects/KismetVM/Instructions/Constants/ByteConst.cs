using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class ByteConst : ConstToken<byte>
    {
        public override EExprToken Expr => EExprToken.EX_ByteConst;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _value);
        }
    }
}
