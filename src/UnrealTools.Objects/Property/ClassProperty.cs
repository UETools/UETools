using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;
using UnrealTools.Objects.Package;

namespace UnrealTools.Objects.Property
{
    internal sealed class ClassProperty : UProperty<ObjectReference>
    {
        public override void Deserialize(FArchive reader, PropertyTag tag) => reader.Read(out _value);
    }
}