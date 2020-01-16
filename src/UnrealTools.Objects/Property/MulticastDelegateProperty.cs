using UnrealTools.Core;
using UnrealTools.Objects.Enums;
using UnrealTools.Objects.Interfaces;
using UnrealTools.Objects.Package;

namespace UnrealTools.Objects.Property
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