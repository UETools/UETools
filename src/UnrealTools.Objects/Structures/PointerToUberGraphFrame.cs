using System;
using System.Collections.Generic;
using System.Text;
using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    struct PointerToUberGraphFrame : IUnrealStruct
    {
        public void Deserialize(FArchive reader) => reader.Read(out _name);
        public override string ToString() => _name.ToString();
        private FName _name;
    }
}
