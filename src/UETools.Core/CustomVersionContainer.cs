using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UETools.Core;
using UETools.Core.Enums;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    public partial class CustomVersionContainer : IUnrealSerializable
    {
        public CustomVersionContainer(CustomVersionSerializationFormat format) => _type = format;

        internal List<CustomVersion> Versions { get; set; } = null!;
        public FArchive Serialize(FArchive reader)
        {
            switch (_type)
            {
                case CustomVersionSerializationFormat.Guids:
                    {
                        List<GuidCustomVersion> _versions = default!;
                        reader.Read(ref _versions);
                        Versions = new List<CustomVersion>(_versions.Cast<CustomVersion>());
                        break;
                    }
                case CustomVersionSerializationFormat.Enums:
                    {
                        List<EnumCustomVersion> _versions = default!;
                        reader.Read(ref _versions);
                        Versions = new List<CustomVersion>(_versions.Cast<CustomVersion>());
                        break;
                    }
                case CustomVersionSerializationFormat.Optimized:
                    {
                        List<CustomVersion> _versions = default!;
                        reader.Read(ref _versions);
                        Versions = _versions;
                        break;
                    }
                default:
                    throw new UnrealException($"{_type} custom version not implemented");
            }
            return reader;
        }

        private CustomVersionSerializationFormat _type;
    }
}
