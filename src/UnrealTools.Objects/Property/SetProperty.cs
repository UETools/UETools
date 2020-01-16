using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;
using UnrealTools.Objects.Interfaces;
using UnrealTools.TypeFactory;

namespace UnrealTools.Objects.Property
{
    internal sealed class SetProperty : UProperty<HashSet<IProperty>>
    {
        public int Count => _value.Count;

        public override void Deserialize(FArchive reader, PropertyTag tag)
        {
            reader.Read(out int unknown);
            reader.Read(out int count); 
            if (tag.InnerTypeEnum.TryGetAttribute(out LinkedTypeAttribute? attrib))
            {
                _value = new HashSet<IProperty>(count);
                var func = PropertyFactory.Get(attrib.LinkedType);
                for (int i = 0; i < count; i++)
                {
                    var prop = func();
                    prop.Deserialize(reader, tag);
                    _value.Add(prop);
                }
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

            writer.WriteLine('[');
            writer.Indent++;
            var it = _value.GetEnumerator();
            for (int i = 0; it.MoveNext(); i++)
            {
                switch (it.Current)
                {
                    case IUnrealReadable indent:
                        indent.ReadTo(writer);
                        break;
                    default:
                        writer.Write(it);
                        break;
                }

                if (i != count - 1)
                    writer.WriteLine(", ");
            }
            writer.Indent--;
            writer.WriteLine(']');
        }
    }
}