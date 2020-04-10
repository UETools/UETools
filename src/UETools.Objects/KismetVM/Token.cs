using System.CodeDom.Compiler;
using System.IO;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Objects.KismetVM
{
    public abstract class Token : IUnrealDeserializable, IUnrealReadable
    {
        public abstract EExprToken Expr { get; }
        public long TokenOffset { get; private set; }

        public virtual void Deserialize(FArchive reader) => TokenOffset = reader.Tell();

        protected static Token Read(FArchive reader) => TokenFactory.Read(reader);

        public abstract void ReadTo(TextWriter writer);

        public void ReadTo(IndentedTextWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}
