using UnrealTools.Core;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class InstanceDelegate : ConstToken<FName>
    {
        public override EExprToken Expr => EExprToken.EX_InstanceDelegate;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _value);
        }
    }
}
