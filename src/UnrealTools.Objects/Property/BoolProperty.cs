using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Property
{
    internal sealed class BoolProperty : UProperty<bool>
    {
        public override void Deserialize(FArchive reader, PropertyTag tag) => _value = tag.BoolValue;
    }
}
