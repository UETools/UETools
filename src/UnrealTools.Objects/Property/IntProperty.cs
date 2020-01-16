using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Property
{
    internal sealed class IntProperty : UProperty<int>
    {
        public override void Deserialize(FArchive reader, PropertyTag tag) => reader.Read(out _value);
    }
}