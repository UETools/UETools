using System.Diagnostics;
using UETools.Core;
using UETools.Objects.Enums;
using UETools.Objects.Interfaces;
using UETools.Objects.Package;

namespace UETools.Objects.Property
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