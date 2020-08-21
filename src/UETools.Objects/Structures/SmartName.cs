using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct SmartName : IUnrealStruct
    {
        public FArchive Serialize(FArchive reader)
        {
            reader.Read(ref _value);
            if (!reader.EOF())
                reader.Read(ref _uid);
            if (!reader.EOF())
                reader.Read(ref _guid);

            return reader;
        }
        public override string ToString() => _value.ToString();

        private FName _value;
        private short _uid;
        private System.Guid _guid;
    }
}
