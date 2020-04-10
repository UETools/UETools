using UETools.Core;
using UETools.Objects.Package;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class ObjectConst : ConstToken<ObjectReference>
    {
        public override EExprToken Expr => EExprToken.EX_ObjectConst;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _value);
        }
    }
}
