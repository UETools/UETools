using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class SoftObjectConst : ConstToken<Token>
    {
        public override EExprToken Expr => EExprToken.EX_SoftObjectConst;

        public override FArchive Serialize(FArchive reader)
        {
            base.Serialize(reader);
            _value = Token.Read(reader);
            return reader;
        }
    }
}
