using UnrealTools.Core;
using UnrealTools.Core.Interfaces;
using UnrealTools.Objects.Structures;

namespace UnrealTools.KismetVM.Instructions
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
