using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class NameConst : ConstToken<FName>
    {
        public override EExprToken Expr => EExprToken.EX_NameConst;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _value);
        }
    }
}
