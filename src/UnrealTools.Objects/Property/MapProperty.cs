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
    internal sealed class MapProperty : PropertyCollectionBase<Dictionary<IProperty, IProperty>>
    {
        public override void Deserialize(FArchive reader, PropertyTag tag)
        {
            if (tag.InnerTypeEnum.TryGetAttribute(out LinkedTypeAttribute? innerType) && tag.ValueTypeEnum.TryGetAttribute(out LinkedTypeAttribute? valueType))
            {
                reader.Read(out int unknown);
                base.Deserialize(reader, tag);
                var dict = new Dictionary<IProperty, IProperty>(Count);
                var funcKey = PropertyFactory.Get(innerType.LinkedType);
                var valueKey = PropertyFactory.Get(valueType.LinkedType);
                for(int i = 0; i < Count; i++)
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

        protected override void WriteInnerItems(IndentedTextWriter writer)
        {
            var it = _value.GetEnumerator();
            for (var i = 0; it.MoveNext(); i++)
            {
                var obj = it.Current;
                writer.Write("[ ");
                ReadObject(writer, obj.Key);
                writer.WriteLine(" => ");
                ReadObject(writer, obj.Value);
                writer.Write("]");
                if (i != Count - 1)
                    writer.WriteLine(", ");
            }
        }
    }
}