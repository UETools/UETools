using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class NameConst : ConstToken<FName>
    {
        public override EExprToken Expr => EExprToken.EX_NameConst;

        public override FArchive Serialize(FArchive reader) 
            => base.Serialize(reader)
                   .Read(ref _value);
    }
}
