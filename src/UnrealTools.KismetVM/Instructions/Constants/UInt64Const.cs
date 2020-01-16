using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class UInt64Const : ConstToken<ulong>
    {
        public override EExprToken Expr => EExprToken.EX_UInt64Const;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _value);
        }
    }
}
