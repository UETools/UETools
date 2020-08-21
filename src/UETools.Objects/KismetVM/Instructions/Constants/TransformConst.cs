using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Structures;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class TransformConst : ConstToken<Transform>
    {
        public override EExprToken Expr => EExprToken.EX_TransformConst;

        public override FArchive Serialize(FArchive reader) 
            => base.Serialize(reader)
                   .Read(ref _value);
    }
}
