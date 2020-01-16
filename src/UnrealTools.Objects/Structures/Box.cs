using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    public struct Box : IUnrealStruct
    {
        private bool IsValid => _isValid != 0;

        public void Deserialize(FArchive reader)
        {
            reader.Read(out _min);
            reader.Read(out _max);
            reader.Read(out _isValid);
        }
        public override string ToString() => IsValid ? $"{{ Min: {_min}, Max: {_max} }}" : "{ Invalid Box }";

        private Vector _min;
        private Vector _max;
        private byte _isValid;
    }
}
