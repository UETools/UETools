using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;
using UnrealTools.Objects.Structures;

namespace UnrealTools.Objects.Property
{
    internal sealed class SoftObjectProperty : UProperty<SoftObjectPath>
    {
        public override void Deserialize(FArchive reader, PropertyTag tag) => reader.Read(out _value);
    }
}