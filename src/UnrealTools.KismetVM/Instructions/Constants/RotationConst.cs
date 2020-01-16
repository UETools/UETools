using UnrealTools.Core;
using UnrealTools.Objects.Structures;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class RotationConst : ConstToken<Rotator>
    {
        public override EExprToken Expr => EExprToken.EX_RotationConst;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _value);
        }
    }
}
