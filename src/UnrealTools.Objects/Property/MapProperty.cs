using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;
using UnrealTools.Objects.Interfaces;
using UnrealTools.TypeFactory;

namespace UnrealTools.Objects.Property
{
    internal sealed class MapProperty : UProperty<Dictionary<IProperty, IProperty>>
    {
        int Count => _value.Count;
        public override void Deserialize(FArchive reader, PropertyTag tag)
        {
            if (tag.InnerTypeEnum.TryGetAttribute(out LinkedTypeAttribute? innerType) && tag.ValueTypeEnum.TryGetAttribute(out LinkedTypeAttribute? valueType))
            {
                reader.Read(out int unknown);
                reader.Read(out int count);
                var dict = new Dictionary<IProperty, IProperty>(count);
                var funcKey = PropertyFactory.Get(innerType.LinkedType);
                var valueKey = PropertyFactory.Get(valueType.LinkedType);
                for(int i = 0; i < count; i++)
                {
                    var key = funcKey();
                    key.Deserialize(reader, tag);
                    var value = valueKey();
                    value.Deserialize(reader, tag);
                    dict.Add(key, value);
                }
                _value = dict;
            }
        }

        public override void ReadTo(IndentedTextWriter writer)
        {
            var count = Count;
            if (count == 0)
            {
                writer.WriteLine("[ ]");
                return;
            }

            writer.WriteLine('(');
            writer.Indent++;
            var it = _value.GetEnumerator();
            for (int i = 0; it.MoveNext(); i++)
            {
                var obj = it.Current;
                writer.Write("[ ");
                ReadObject(writer, obj.Key);
                writer.WriteLine(" => ");
                ReadObject(writer, obj.Value);
                writer.Write("]");
                if (i != count - 1)
                    writer.WriteLine(", ");
            }
            writer.Indent--;
            writer.WriteLine(')');
        }

    }
}