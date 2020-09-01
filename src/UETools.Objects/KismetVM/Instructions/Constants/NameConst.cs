using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class NameConst : ConstToken<FName>
    {
        public override EExprToken Expr => EExprToken.EX_NameConst;

        public override FArchive Serialize(FArchive archive)
            => base.Serialize(archive)
                   .Read(ref _value);
    }
}
