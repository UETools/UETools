using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Property
{
    abstract class PropertyCollectionBase<T> : UProperty<T> where T : notnull, IEnumerable
    {
        public int Count => _count;

        public override void Deserialize(FArchive reader, PropertyTag tag) => reader.Read(out _count);
        protected virtual void WriteInnerItems(IndentedTextWriter writer)
        {
            var it = _value.GetEnumerator();
            for (var i = 0; it.MoveNext(); i++)
            {
                ReadObject(writer, it.Current);

                if (i != _count - 1)
                    writer.WriteLine(", ");
            }
        }
        public override void ReadTo(IndentedTextWriter writer)
        {
            if (_count == 0)
            {
                writer.WriteLine("[ ]");
                return;
            }

            writer.WriteLine('[');
            writer.Indent++;
            WriteInnerItems(writer);
            writer.Indent--;
            writer.WriteLine(']');
        }

        private int _count;
    }
}
