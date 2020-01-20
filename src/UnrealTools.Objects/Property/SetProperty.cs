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
    internal sealed class SetProperty : PropertyCollectionBase<HashSet<IProperty>>
    {
        public override void Deserialize(FArchive reader, PropertyTag tag)
        {
            reader.Read(out int unknown);
            base.Deserialize(reader, tag);
            if (tag.InnerTypeEnum.TryGetAttribute(out LinkedTypeAttribute? attrib))
            {
                _value = new HashSet<IProperty>(Count);
                var func = PropertyFactory.Get(attrib.LinkedType);
                for (int i = 0; i < Count; i++)
                {
                    var prop = func();
                    prop.Deserialize(reader, tag);
                    _value.Add(prop);
                }
            }
        }
    }
}