using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    public struct SmartName : IUnrealStruct
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _value);
            if (!reader.EOF())
                reader.Read(out _uid);
            if (!reader.EOF())
                reader.Read(out _guid);
        }
        public override string ToString() => _value.ToString();

        private FName _value;
        private short _uid;
        private System.Guid _guid;
    }
}
