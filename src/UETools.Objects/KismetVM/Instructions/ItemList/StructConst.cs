using System;
using System.IO;
using UETools.Core;
using UETools.Objects.Package;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class StructConst : CollectionToken
    {
        public override EExprToken Expr => EExprToken.EX_StructConst;

        public ObjectReference InnerProp => _innerProp;
        public int SerializedSize => _serializedSize;
        public TokenList Items { get; } = new TokenList();

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive)
                .Read(ref _innerProp)
                .Read(ref _serializedSize);
            Items.ReadUntil(archive, EExprToken.EX_EndStructConst);
            return archive;
        }

        public override void ReadTo(TextWriter writer) => throw new NotImplementedException();

        private ObjectReference _innerProp = null!;
        private int _serializedSize;
    }
}
