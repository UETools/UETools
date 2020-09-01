using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Property
{
    internal sealed class BoolProperty : UProperty<bool>
    {
        public override FArchive Serialize(FArchive reader, PropertyTag tag)
        {
            _value = tag.BoolValue;
            return reader;
        }
    }
}
