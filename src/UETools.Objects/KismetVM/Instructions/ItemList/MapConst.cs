using System;
using System.IO;
using UETools.Core;
using UETools.Objects.Package;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class MapConst : CollectionToken
    {
        public override EExprToken Expr => EExprToken.EX_MapConst;

        public ObjectReference KeyProperty => _keyProperty;
        public ObjectReference ValueProperty => _valueProperty;
        public int Count => _count;
        public TokenList Items { get; } = new TokenList();

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive)
                .Read(ref _keyProperty)
                .Read(ref _valueProperty)
                .Read(ref _count);
            Items.ReadUntil(archive, EExprToken.EX_EndMapConst);
            return archive;
        }

        public override void ReadTo(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        private ObjectReference _keyProperty = null!;
        private ObjectReference _valueProperty = null!;
        private int _count;
    }
}
