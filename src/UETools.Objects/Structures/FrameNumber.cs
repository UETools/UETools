using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct FrameNumber : IUnrealStruct
    {
        public void Deserialize(FArchive reader) => reader.Read(out _value);
        int _value;
    }
}
