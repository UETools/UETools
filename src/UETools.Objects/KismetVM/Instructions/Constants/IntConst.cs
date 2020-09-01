using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class IntConst : ConstToken<int>
    {
        public override EExprToken Expr => EExprToken.EX_IntConst;

        public override FArchive Serialize(FArchive archive)
            => base.Serialize(archive)
                   .Read(ref _value);
    }
}
