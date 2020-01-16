using UnrealTools.Core;
using UnrealTools.Core.Collections;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    public struct MovieSceneFrameRange : IUnrealStruct
    {
        public void Deserialize(FArchive reader) => reader.Read(out _range);
        private TRange<FrameNumber> _range;
    } 
}
