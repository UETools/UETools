using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class SoftObjectConst : ConstToken<Token>
    {
        public override EExprToken Expr => EExprToken.EX_SoftObjectConst;

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);
            _value = Token.Read(archive);
            return archive;
        }
    }
}
