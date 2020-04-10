using UETools.Core;
using UETools.Objects.Enums;
using UETools.Objects.Interfaces;
using UETools.Objects.Package;

namespace UETools.Objects.Property
{
    internal sealed class MulticastDelegateProperty : UProperty<FName>
    {
        public override void Deserialize(FArchive reader, PropertyTag tag)
        {
            reader.Read(out ObjectReference declaredIn);
            if (declaredIn.Resource != null)
            {
                reader.ReadUnsafe(out EFunctionFlags flags);
                reader.Read(out _value);
            }
        }
    }
}