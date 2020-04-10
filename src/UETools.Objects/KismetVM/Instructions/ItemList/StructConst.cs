using System;
using System.IO;
using UETools.Core;
using UETools.Objects.Package;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class StructConst : ListToken
    {
        public override EExprToken Expr => EExprToken.EX_StructConst;

        public ObjectReference InnerProp => _innerProp;
        public int SerializedSize => _serializedSize;
        public TokenList Items { get; } = new TokenList();

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _innerProp);
            reader.Read(out _serializedSize);
            Items.ReadUntil(reader, EExprToken.EX_EndStructConst);
        }

        public override void ReadTo(TextWriter writer) => throw new NotImplementedException();

        private ObjectReference _innerProp = null!;
        private int _serializedSize;
    }
}
