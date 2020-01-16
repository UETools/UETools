using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Property
{
    internal sealed class DoubleProperty : UProperty<double>
    {
        public override void Deserialize(FArchive reader, PropertyTag tag) => reader.Read(out _value);
    }
}