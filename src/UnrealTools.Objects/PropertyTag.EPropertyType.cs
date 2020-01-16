using UnrealTools.Core;
using UnrealTools.Objects.Property;

namespace UnrealTools.Objects
{
    public partial class PropertyTag
    {
        public enum PropertyType : byte
        {
            None = 0,
            [LinkedType(typeof(ByteProperty))]
            ByteProperty = 1,
            [LinkedType(typeof(Int8Property))]
            Int8Property = 2,
            [LinkedType(typeof(Int16Property))]
            Int16Property = 3,
            [LinkedType(typeof(UInt16Property))]
            UInt16Property = 4,
            [LinkedType(typeof(IntProperty))]
            IntProperty = 5,
            [LinkedType(typeof(UInt32Property))]
            UInt32Property = 6,
            [LinkedType(typeof(Int64Property))]
            Int64Property = 7,
            [LinkedType(typeof(UInt64Property))]
            UInt64Property = 8,
            [LinkedType(typeof(FloatProperty))]
            FloatProperty = 9,
            [LinkedType(typeof(BoolProperty))]
            BoolProperty = 10,

            [LinkedType(typeof(NameProperty))]
            NameProperty = 11,
            [LinkedType(typeof(EnumProperty))]
            EnumProperty = 12,
            [LinkedType(typeof(StrProperty))]
            StrProperty = 13,
            [LinkedType(typeof(TextProperty))]
            TextProperty = 14,

            [LinkedType(typeof(InterfaceProperty))]
            InterfaceProperty = 15,
            [LinkedType(typeof(ObjectProperty))]
            ObjectProperty = 16,
            [LinkedType(typeof(StructProperty))]
            StructProperty = 17,

            [LinkedType(typeof(ArrayProperty))]
            ArrayProperty = 18,
            [LinkedType(typeof(SetProperty))]
            SetProperty = 19,
            [LinkedType(typeof(MapProperty))]
            MapProperty = 20,

            [LinkedType(typeof(SoftObjectProperty))]
            SoftObjectProperty = 21,
            [LinkedType(typeof(SoftClassProperty))]
            SoftClassProperty = 22,

            [LinkedType(typeof(DelegateProperty))]
            DelegateProperty = 23,
            [LinkedType(typeof(MulticastDelegateProperty))]
            MulticastDelegateProperty = 24,
            [LinkedType(typeof(MulticastInlineDelegateProperty))]
            MulticastInlineDelegateProperty = 25,
            [LinkedType(typeof(MulticastSparseDelegateProperty))]
            MulticastSparseDelegateProperty = 26,


        }
    }
}
