using UETools.Core;
using UETools.Objects.Structures;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class RotationConst : ConstToken<Rotator>
    {
        public override EExprToken Expr => EExprToken.EX_RotationConst;

        public override FArchive Serialize(FArchive archive)
            => base.Serialize(archive)
                   .Read(ref _value);
    }
}
