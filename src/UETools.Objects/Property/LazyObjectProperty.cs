using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Property
{
    internal sealed class LazyObjectProperty : UProperty<int>
    {
        public override FArchive Serialize(FArchive reader, PropertyTag tag) => reader.Read(ref _value);
    }
}
