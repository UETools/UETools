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
    internal sealed class ArrayProperty : PropertyCollectionBase<IList>
    {
        public override void Deserialize(FArchive reader, PropertyTag tag)
        {
            base.Deserialize(reader, tag);
            var info = tag;
            if (tag.InnerTypeEnum == PropertyTag.PropertyType.StructProperty)
                reader.Read(out info);

            if ((tag.InnerTypeEnum == PropertyTag.PropertyType.ByteProperty && tag.EnumName is null) || tag.InnerTypeEnum == PropertyTag.PropertyType.BoolProperty)
            {
                reader.Read(out Memory<byte> bytes, Count);
                _value = bytes.ToArray();
            }
            else
            {
                if (tag.InnerTypeEnum.TryGetAttribute(out LinkedTypeAttribute? attrib))
                {
                    var array = new List<IProperty>(Count);
                    var func = PropertyFactory.Get(attrib.LinkedType);
                    for (var i = 0; i < Count; i++)
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
                base.ReadTo(writer);
        }
    }
}