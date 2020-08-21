using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class IntConst : ConstToken<int>
    {
        public override EExprToken Expr => EExprToken.EX_IntConst;

        public override FArchive Serialize(FArchive reader) 
            => base.Serialize(reader)
                   .Read(ref _value);
    }
}
