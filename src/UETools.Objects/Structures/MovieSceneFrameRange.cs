using UETools.Core;
using UETools.Core.Collections;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct MovieSceneFrameRange : IUnrealStruct
    {
        public FArchive Serialize(FArchive reader) => reader.Read(ref _range);

        private TRange<FrameNumber> _range;
    } 
}
