using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct FrameNumber : IUnrealStruct
    {
        public FArchive Serialize(FArchive archive) => archive.Read(ref _value);

        int _value;
    }
}
