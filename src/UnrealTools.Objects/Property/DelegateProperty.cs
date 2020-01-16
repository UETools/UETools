using System.Diagnostics;
using UnrealTools.Core;
using UnrealTools.Objects.Enums;
using UnrealTools.Objects.Interfaces;
using UnrealTools.Objects.Package;

namespace UnrealTools.Objects.Property
{
    internal sealed class DelegateProperty : UProperty<FName>
    {
        public override void Deserialize(FArchive reader, PropertyTag tag)
        {
            reader.Read(out ObjectReference declaredIn);
            if (tag.Size > 4)
                reader.Read(out _value);
            else 
                Debugger.Break();
        }
    }
}