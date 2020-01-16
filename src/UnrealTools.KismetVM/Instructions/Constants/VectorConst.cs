using UnrealTools.Core;
using UnrealTools.Objects.Structures;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed class VectorConst : ConstToken<Vector>
    {
        public override EExprToken Expr => EExprToken.EX_VectorConst;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _value);
        }
    }
}
