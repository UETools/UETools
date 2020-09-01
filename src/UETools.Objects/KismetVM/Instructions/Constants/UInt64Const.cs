using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class UInt64Const : ConstToken<ulong>
    {
        public override EExprToken Expr => EExprToken.EX_UInt64Const;

        public override FArchive Serialize(FArchive archive)
            => base.Serialize(archive)
                   .Read(ref _value);
    }
}
