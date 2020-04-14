using System;
using System.IO;
using UETools.Core;
using UETools.Objects.Package;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class SetConst : CollectionToken
    {
        public override EExprToken Expr => EExprToken.EX_SetConst;

        public ObjectReference InnerProp => _innerProp;
        public int Count => _count;
        public TokenList Items { get; } = new TokenList();

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _innerProp);
            reader.Read(out _count);
            Items.ReadUntil(reader, EExprToken.EX_EndSetConst);
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private ObjectReference _innerProp = null!;
        private int _count;
    }
}
