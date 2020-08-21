using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct FontCharacter : IUnrealStruct
    {
        public FArchive Serialize(FArchive reader) 
            => reader.Read(ref _startU)
                     .Read(ref _startV)
                     .Read(ref _uSize)
                     .Read(ref _vSize)
                     .Read(ref _textureIndex)
                     .Read(ref _verticalOffset);

        public override string? ToString()
        {
            return base.ToString();
        }

        private int _startU;
        private int _startV;
        private int _uSize;
        private int _vSize;
        private byte _textureIndex;
        private int _verticalOffset;
    }
}
