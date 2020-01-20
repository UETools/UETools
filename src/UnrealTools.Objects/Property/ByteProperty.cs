using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Property
{
    internal sealed class ByteProperty : UProperty<object>
    {
        public override void Deserialize(FArchive reader, PropertyTag tag)
        {
            if (tag.EnumName!.IsNone())
            {
                reader.Read(out byte underlying);
                _value = underlying;
            }
            else
            {
                reader.Read(out FName name);
                _value = name;
            }
        }
    }
}
