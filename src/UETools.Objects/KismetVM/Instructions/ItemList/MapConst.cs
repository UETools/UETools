using System;
using System.IO;
using UETools.Core;
using UETools.Objects.Package;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class MapConst : ListToken
    {
        public override EExprToken Expr => EExprToken.EX_MapConst;

        public ObjectReference KeyProperty => _keyProperty;
        public ObjectReference ValueProperty => _valueProperty;
        public int Count => _count;
        public TokenList Items { get; } = new TokenList();

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _keyProperty);
            reader.Read(out _valueProperty);
            reader.Read(out _count);
            Items.ReadUntil(reader, EExprToken.EX_EndMapConst);
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
