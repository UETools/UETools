using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Property
{
    internal sealed class BoolProperty : UProperty<bool>
    {
        public override void Deserialize(FArchive reader, PropertyTag tag) => _value = tag.BoolValue;
    }
}
