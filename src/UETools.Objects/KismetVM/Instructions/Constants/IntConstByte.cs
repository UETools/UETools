using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class IntConstByte : ConstToken<int>
    {
        public override EExprToken Expr => EExprToken.EX_IntConstByte;

        public override FArchive Serialize(FArchive reader)
        {
            base.Serialize(reader);
            byte val = 0;
            reader.Read(ref val);
            _value = val;
            return reader;
        }
    }
}
