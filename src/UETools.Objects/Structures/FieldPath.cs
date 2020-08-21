using System.Collections.Generic;
using UETools.Core;
using UETools.Objects.Interfaces;
using UETools.Objects.Package;

namespace UETools.Objects.Property
{
    internal struct FieldPath : IUnrealStruct
    {
        public FArchive Serialize(FArchive archive)
            => archive.Read(ref _path)
                      .Read(ref _reference);


        List<FName> _path; 
        ObjectReference _reference;
    }
}