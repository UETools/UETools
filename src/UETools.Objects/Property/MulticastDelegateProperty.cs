using UETools.Core;
using UETools.Objects.Enums;
using UETools.Objects.Interfaces;
using UETools.Objects.Package;

namespace UETools.Objects.Property
{
    internal sealed class MulticastDelegateProperty : UProperty<FName>
    {
        public override FArchive Serialize(FArchive reader, PropertyTag tag)
        {
            ObjectReference? declaredIn = default;
            reader.Read(ref declaredIn);
            if (declaredIn.Resource != null)
            {
                EFunctionFlags flags = default;
                reader.ReadUnsafe(ref flags);
                reader.Read(ref _value);
            }
            return reader;
        }
    }
}