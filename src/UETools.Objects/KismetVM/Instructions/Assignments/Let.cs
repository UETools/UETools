using UETools.Core;
using UETools.Objects.Package;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class Let : LetToken
    {
        public override EExprToken Expr => EExprToken.EX_Let;

        public override void Deserialize(FArchive reader)
        {
            reader.Read(out _property);
            base.Deserialize(reader);
        }

        ObjectReference _property = null!;
    }
}
