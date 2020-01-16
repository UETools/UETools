using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    public struct SmartNameShort : IUnrealStruct
    {
        public void Deserialize(FArchive reader) => reader.Read(out _value);
        public override string ToString() => _value.ToString();

        private FName _value;
    }
}
