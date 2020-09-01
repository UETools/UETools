using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class IntConstByte : ConstToken<int>
    {
        public override EExprToken Expr => EExprToken.EX_IntConstByte;

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);
            byte val = 0;
            archive.Read(ref val);
            _value = val;
            return archive;
        }
    }
}
