using System;
using System.IO;
using UETools.Core;
using UETools.Objects.Package;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class ArrayConst : CollectionToken
    {
        public override EExprToken Expr => EExprToken.EX_ArrayConst;

        public ObjectReference InnerProp => _innerProp;
        public int Count => _count;
        public TokenList Items { get; } = new TokenList();

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive)
                .Read(ref _innerProp)
                .Read(ref _count);

            Items.ReadUntil(archive, EExprToken.EX_EndArrayConst);
            return archive;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private ObjectReference _innerProp = null!;
        private int _count;
    }
}
