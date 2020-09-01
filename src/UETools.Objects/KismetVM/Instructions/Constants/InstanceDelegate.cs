using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class InstanceDelegate : ConstToken<FName>
    {
        public override EExprToken Expr => EExprToken.EX_InstanceDelegate;

        public override FArchive Serialize(FArchive archive)
            => base.Serialize(archive)
                   .Read(ref _value);
    }
}
