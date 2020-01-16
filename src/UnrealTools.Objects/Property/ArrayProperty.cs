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
    internal sealed class ArrayProperty : UProperty<IList>
    {
        public int Count => _value.Count;

        public override void Deserialize(FArchive reader, PropertyTag tag)
        {
            reader.Read(out int count);
            var info = tag;
            if (tag.InnerTypeEnum == PropertyTag.PropertyType.StructProperty)
                reader.Read(out info);

            if ((tag.InnerTypeEnum == PropertyTag.PropertyType.ByteProperty && tag.EnumName is null) || tag.InnerTypeEnum == PropertyTag.PropertyType.BoolProperty)
            {
                reader.Read(out Memory<byte> bytes, count);
                _value = bytes.ToArray();
            }
            else
            {
                if (tag.InnerTypeEnum.TryGetAttribute(out LinkedTypeAttribute? attrib))
                {
                    var array = new List<IProperty>(count);
                    var func = PropertyFactory.Get(attrib.LinkedType);

                    if (count == 1 && tag.InnerTypeEnum == PropertyTag.PropertyType.StructProperty && info.StructName == "ClothLODData")
                        Debugger.Break();

                    for (var i = 0; i < count; i++, info.ArrayIndex = i)
                    {
                        var prop = func();
                        prop.Deserialize(reader, info);
                        array.Add(prop);
                    }
                    _value = array;
                }
            }
        }

        public override void ReadTo(IndentedTextWriter writer)
        {
            if (_value is byte[] bytes)
                writer.WriteLine(new StringBuilder().Append("[ ").AppendJoin(", ", bytes).Append(" ]"));
            else
            {
                var count = Count;
                if (count == 0)
                {
                    writer.WriteLine("[ ]");
                    return;
                }

                writer.WriteLine('[');
                writer.Indent++;
                for(int i = 0; i < count; i++)
                {
                    var it = _value[i];
                    ReadObject(writer, it);
                    if (i != count - 1)
                        writer.WriteLine(", ");
                }
                writer.Indent--;
                writer.WriteLine(']');
            }
        }
    }
}