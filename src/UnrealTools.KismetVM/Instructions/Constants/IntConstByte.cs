using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class IntConstByte : ConstToken<int>
    {
        public override EExprToken Expr => EExprToken.EX_IntConstByte;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out byte val);
            _value = val;
        }
    }
}
