using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class NoInterface : ConstToken<object?>
    {
        public override EExprToken Expr => EExprToken.EX_NoInterface;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            _value = null;
        }
    }
}
