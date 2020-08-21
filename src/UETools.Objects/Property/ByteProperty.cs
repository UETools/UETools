using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Property
{
    internal sealed class ByteProperty : UProperty<object>
    {
        public override FArchive Serialize(FArchive reader, PropertyTag tag)
        {
            if (tag.EnumName!.IsNone())
            {
                byte underlying = 0;
                reader.Read(ref underlying);
                _value = underlying;
            }
            else
            {
                FName? name = default;
                reader.Read(ref name);
                _value = name;
            }
            return reader;
        }
    }
}
