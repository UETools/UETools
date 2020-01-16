using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    public struct SmartNameGuid : IUnrealStruct
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _value);
            reader.Read(out _uid);
            reader.Read(out Guid _guid);
        }
        public override string ToString() => _value.ToString();

        private FName _value;
        private short _uid;
    }
}
