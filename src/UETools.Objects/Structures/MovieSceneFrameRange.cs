using UETools.Core;
using UETools.Core.Collections;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct MovieSceneFrameRange : IUnrealStruct
    {
        public void Deserialize(FArchive reader) => reader.Read(out _range);
        private TRange<FrameNumber> _range;
    } 
}
