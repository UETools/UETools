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
    abstract class PropertyCollectionBase<T> : UProperty<T> where T : ICollection
    {
        public int Count => _value.Count;

        public override void ReadTo(IndentedTextWriter writer)
        {
            var count = Count;
            if (count == 0)
            {
                writer.WriteLine("[ ]");
                return;
            }

            writer.WriteLine('[');
            writer.Indent++;
            for (int i = 0; i < count; i++)
            {
                var it = _value;
                ReadObject(writer, it);
                if (i != count - 1)
                    writer.WriteLine(", ");
            }
            writer.Indent--;
            writer.WriteLine(']');
        }
    }
}
