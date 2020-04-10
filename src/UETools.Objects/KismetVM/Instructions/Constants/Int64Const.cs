using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class Int64Const : ConstToken<long>
    {
        public override EExprToken Expr => EExprToken.EX_Int64Const;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _value);
        }
    }
}
