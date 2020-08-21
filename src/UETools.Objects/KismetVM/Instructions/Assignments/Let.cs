using UETools.Core;
using UETools.Objects.Package;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class Let : LetToken
    {
        public override EExprToken Expr => EExprToken.EX_Let;

        public override FArchive Serialize(FArchive archive)
        {
            archive.Read(ref _property);
            return base.Serialize(archive);
        }

        ObjectReference _property = null!;
    }
}
