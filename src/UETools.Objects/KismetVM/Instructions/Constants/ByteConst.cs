using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class ByteConst : ConstToken<byte>
    {
        public override EExprToken Expr => EExprToken.EX_ByteConst;

        public override FArchive Serialize(FArchive archive)
            => base.Serialize(archive)
                   .Read(ref _value);
    }
}
