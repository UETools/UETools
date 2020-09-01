using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct SmartName : IUnrealStruct
    {
        public FArchive Serialize(FArchive archive)
        {
            archive.Read(ref _value);
            if (!archive.EOF())
                archive.Read(ref _uid);
            if (!archive.EOF())
                archive.Read(ref _guid);

            return archive;
        }
        public override string ToString() => _value.ToString();

        private FName _value;
        private short _uid;
        private System.Guid _guid;
    }
}
