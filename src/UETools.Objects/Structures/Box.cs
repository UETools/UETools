using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct Box : IUnrealStruct
    {
        public bool IsValid => _isValid != 0;
        public Vector Min => _min;
        public Vector Max => _max;


        public FArchive Serialize(FArchive reader) 
            => reader.Read(ref _min)
                     .Read(ref _max)
                     .Read(ref _isValid);

        public override string ToString() => IsValid ? $"{{ Min: {_min}, Max: {_max} }}" : $"{{ Invalid {nameof(Box)} }}";

        private Vector _min;
        private Vector _max;
        private byte _isValid;
    }
}
