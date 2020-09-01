using UETools.Core;
using UETools.Objects.Enums;
using UETools.Objects.Interfaces;
using UETools.Objects.Package;

namespace UETools.Objects.Property
{
    internal sealed class MulticastInlineDelegateProperty : UProperty<FName>
    {
        public override FArchive Serialize(FArchive reader, PropertyTag tag)
        {
            ObjectReference? declaredIn = default;
            EFunctionFlags flags = default;
            return reader.Read(ref declaredIn)
                         .ReadUnsafe(ref flags)
                         .Read(ref _value);
        }
    }
}