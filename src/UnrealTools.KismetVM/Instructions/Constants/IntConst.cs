using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class IntConst : ConstToken<int>
    {
        public override EExprToken Expr => EExprToken.EX_IntConst;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _value);
        }
    }
}
