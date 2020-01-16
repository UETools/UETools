using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class SoftObjectConst : ConstToken<Token>
    {
        public override EExprToken Expr => EExprToken.EX_SoftObjectConst;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            _value = Token.Read(reader);
        }
    }
}
