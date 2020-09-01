using System.CodeDom.Compiler;
using System.IO;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Objects.KismetVM
{
    public abstract class Token : IUnrealSerializable, IUnrealReadable
    {
        public abstract EExprToken Expr { get; }
        public long TokenOffset { get; private set; }

        public virtual FArchive Serialize(FArchive archive)
        {
            TokenOffset = archive.Tell();
            return archive;
        }

        protected static Token Read(FArchive reader) => TokenFactory.Read(reader);

        public abstract void ReadTo(TextWriter writer);

        public void ReadTo(IndentedTextWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}
