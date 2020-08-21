using UETools.Core;
using UETools.Objects.Interfaces;
using UETools.Objects.Structures;

namespace UETools.Objects.Property
{
    internal sealed class SoftClassProperty : UProperty<SoftClassPath>
    {
        public override FArchive Serialize(FArchive reader, PropertyTag tag) => reader.Read(ref _value);
    }
}