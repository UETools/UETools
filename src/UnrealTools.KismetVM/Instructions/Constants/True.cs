using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class True : ConstToken<bool>
    {
        public override EExprToken Expr => EExprToken.EX_True;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            _value = true;
        }
    }
}
