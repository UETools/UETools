using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct Box2D : IUnrealStruct
    {
        private bool IsValid => _isValid != 0;

        public void Deserialize(FArchive reader)
        {
            reader.Read(out _min);
            reader.Read(out _max);
            reader.Read(out _isValid);
        }
        public override string ToString() => IsValid ? $"{{ Min: {_min}, Max: {_max} }}" : $"{{ Invalid {nameof(Box2D)} }}";

        private Vector2D _min;
        private Vector2D _max;
        private byte _isValid;
    }
}
