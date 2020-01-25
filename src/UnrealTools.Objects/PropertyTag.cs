using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using UnrealTools.Core;
using UnrealTools.Core.Enums;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Objects
{
    [DebuggerDisplay("{TypeEnum} {Name}")]
    public partial class PropertyTag : IUnrealDeserializable
    {
        public FName Name { get => _name; set => _name = value; }
        public FName Type { get => _type; set => _type = value; }

        public int Size { get => _size; set => _size = value; }
        public int ArrayIndex { get => _arrayIndex; set => _arrayIndex = value; }

        public bool BoolValue { get => _boolVal != 0; set => _boolVal = (byte)(value ? 1 : 0); }
        public FName? StructName { get => _structName; set => _structName = value; }
        public FName? EnumName { get => _enumName; set => _enumName = value; }
        public FName? InnerType { get => _innerType; set => _innerType = value; }
        public FName? ValueType { get => _valueType; set => _valueType = value; }

        public PropertyType TypeEnum => _typeEnum;
        public PropertyType InnerTypeEnum => _innerTypeEnum;
        public PropertyType ValueTypeEnum => _valueTypeEnum;

        public long PropertyEnd { get; private set; }

        public void Deserialize(FArchive reader)
        {
            var version = reader.Version;
            reader.Read(out _name);
            if (Name.IsNone())
                return;

            reader.Read(out _type);

            var foundType = Enum.TryParse(Type, out _typeEnum);
            Debug.WriteLineIf(!foundType, $"Unimplemented property type: {_type}");

            reader.Read(out _size);
            reader.Read(out _arrayIndex);

            switch (TypeEnum)
            {
                case PropertyType.StructProperty:
                    reader.Read(out _structName);
                    if (version >= UE4Version.VER_UE4_STRUCT_GUID_IN_PROPERTY_TAG)
                    {
                        reader.Read(out _structGuid);
                    }
                    break;
                case PropertyType.BoolProperty:
                    reader.Read(out _boolVal);
                    break;
                case PropertyType.ByteProperty:
                case PropertyType.EnumProperty:
                    reader.Read(out _enumName);
                    break;
                case PropertyType.ArrayProperty when version >= UE4Version.VAR_UE4_ARRAY_PROPERTY_INNER_TAGS:
                case PropertyType.SetProperty when version >= UE4Version.VER_UE4_PROPERTY_TAG_SET_MAP_SUPPORT:
                    reader.Read(out _innerType);
                    break;
                case PropertyType.MapProperty when version >= UE4Version.VER_UE4_PROPERTY_TAG_SET_MAP_SUPPORT:
                    reader.Read(out _innerType);
                    reader.Read(out _valueType);
                    break;
            }

            if (version >= UE4Version.VER_UE4_PROPERTY_GUID_IN_PROPERTY_TAG)
            {
                reader.Read(out _hasPropertyGuid);
                if (_hasPropertyGuid != 0)
                {
                    reader.Read(out _propertyGuid);
                }
            }


            PropertyEnd = reader.Tell() + Size;

            if (_innerType is null)
                return;
            var foundInner = Enum.TryParse(_innerType, out _innerTypeEnum);
            Debug.WriteLineIf(!foundInner, $"Unimplemented inner type: {_innerType}");
            if (_valueType is null)
                return;
            var foundValue = Enum.TryParse(_valueType, out _valueTypeEnum);
            Debug.WriteLineIf(!foundValue, $"Unimplemented value type: {_valueType}");
        }

        public static IEnumerable<PropertyTag> ReadToEnd(FArchive reader)
        {
            while (true)
            {
                reader.Read(out PropertyTag tag);
                if (tag.Name.IsNone())
                    break;

                yield return tag;
            }
        }

        private FName _name = null!;
        private FName _type = null!;
        private byte _boolVal;
        private FName? _structName;
        private FName? _enumName;
        private FName? _innerType;
        private FName? _valueType;
        private int _size;
        private int _arrayIndex;
        private Guid _structGuid;
        private byte _hasPropertyGuid;
        private Guid _propertyGuid;

        private PropertyType _typeEnum;
        private PropertyType _innerTypeEnum;
        private PropertyType _valueTypeEnum;
    }
}
