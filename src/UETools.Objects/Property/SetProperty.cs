using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Interfaces;
using UETools.TypeFactory;

namespace UETools.Objects.Property
{
    internal sealed class SetProperty : PropertyCollectionBase<HashSet<IProperty>>
    {
        public override FArchive Serialize(FArchive reader, PropertyTag tag)
        {
            int unknown = 0;
            reader.Read(ref unknown);
            base.Serialize(reader, tag);
            if (tag.InnerTypeEnum.TryGetAttribute(out LinkedTypeAttribute? attrib))
            {
                _value = new HashSet<IProperty>(Count);
                var func = PropertyFactory.Get(attrib.LinkedType);
                for (int i = 0; i < Count; i++)
                {
                    var prop = func();
                    prop.Serialize(reader, tag);
                    _value.Add(prop);
                }
            }
            return reader;
        }
    }
}