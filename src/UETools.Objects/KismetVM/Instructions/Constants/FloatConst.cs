using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class FloatConst : ConstToken<float>
    {
        public override EExprToken Expr => EExprToken.EX_FloatConst;

        public override FArchive Serialize(FArchive archive)
            => base.Serialize(archive)
                   .Read(ref _value);
    }
}
