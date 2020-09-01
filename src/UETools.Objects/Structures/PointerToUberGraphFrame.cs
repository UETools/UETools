using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    struct PointerToUberGraphFrame : IUnrealStruct
    {
        public FArchive Serialize(FArchive archive) => archive.Read(ref _name);

        public override string ToString() => _name.ToString();
        private FName _name;
    }
}
