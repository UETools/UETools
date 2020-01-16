using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;
using UnrealTools.Objects.Structures;

namespace UnrealTools.Objects.Property
{
    internal sealed class SoftClassProperty : UProperty<SoftClassPath>
    {
        public override void Deserialize(FArchive reader, PropertyTag tag) => reader.Read(out _value);
    }
}