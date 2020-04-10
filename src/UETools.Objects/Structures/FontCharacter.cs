using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct FontCharacter : IUnrealStruct
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _startU);
            reader.Read(out _startV);
            reader.Read(out _uSize);
            reader.Read(out _vSize);
            reader.Read(out _textureIndex);
            reader.Read(out _verticalOffset);
        }

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
