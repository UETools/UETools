using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Structures;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class TransformConst : ConstToken<Transform>
    {
        public override EExprToken Expr => EExprToken.EX_TransformConst;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _value);
        }
    }
}
