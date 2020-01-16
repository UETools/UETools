using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class NoObject : ConstToken<object?>
    {
        public override EExprToken Expr => EExprToken.EX_NoObject;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            _value = null;
        }
    }
}
