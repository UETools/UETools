using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    public struct FrameNumber : IUnrealStruct
    {
        public void Deserialize(FArchive reader) => reader.Read(out _value);
        int _value;
    }
}
