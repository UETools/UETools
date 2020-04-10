using UETools.Core;
using UETools.Objects.Interfaces;
using UETools.Objects.Structures;

namespace UETools.Objects.Property
{
    internal sealed class SoftClassProperty : UProperty<SoftClassPath>
    {
        public override void Deserialize(FArchive reader, PropertyTag tag) => reader.Read(out _value);
    }
}