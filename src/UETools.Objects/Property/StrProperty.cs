using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Property
{
    internal sealed class StrProperty : UProperty<FString>
    {
        public override FArchive Serialize(FArchive reader, PropertyTag tag) => reader.Read(ref _value);
    }
}