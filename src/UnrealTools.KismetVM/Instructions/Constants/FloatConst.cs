using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class FloatConst : ConstToken<float>
    {
        public override EExprToken Expr => EExprToken.EX_FloatConst;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _value);
        }
    }
}
