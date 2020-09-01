using System.Diagnostics;
using UETools.Core;
using UETools.Objects.Enums;
using UETools.Objects.Interfaces;
using UETools.Objects.Package;

namespace UETools.Objects.Property
{
    internal sealed class DelegateProperty : UProperty<FName>
    {
        public override FArchive Serialize(FArchive reader, PropertyTag tag)
        {
            ObjectReference? declaredIn = default;
            reader.Read(ref declaredIn);
            if (tag.Size > 4)
                reader.Read(ref _value);
            else 
                Debugger.Break();
            return reader;
        }
    }
}