using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnrealTools.Core;
using UnrealTools.Core.Enums;

namespace UnrealTools.Core
{
    public partial class CustomVersionContainer
    {
        public CustomVersionContainer(CustomVersionSerializationFormat format) => _type = format;

        internal List<CustomVersion> Versions { get; set; } = null!;
        public void Deserialize(FArchive reader)
        {
            switch (_type)
            {
                case CustomVersionSerializationFormat.Guids:
                    {
                        reader.Read(out List<GuidCustomVersion> _versions);
                        Versions = new List<CustomVersion>(_versions.Cast<CustomVersion>());
                        break;
                    }
                case CustomVersionSerializationFormat.Enums:
                    {
                        reader.Read(out List<EnumCustomVersion> _versions);
                        Versions = new List<CustomVersion>(_versions.Cast<CustomVersion>());
                        break;
                    }
                case CustomVersionSerializationFormat.Optimized:
                    {
                        reader.Read(out List<CustomVersion> _versions);
                        Versions = _versions;
                        break;
                    }
                default:
                    throw new UnrealException($"{_type} custom version not implemented");
            }
        }

        private CustomVersionSerializationFormat _type;
    }
}
