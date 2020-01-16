using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    public struct Guid : IUnrealStruct, System.IEquatable<Guid>
    {
        public void Deserialize(FArchive reader) => reader.Read(out _guid);

        public override string ToString() => _guid.ToString();

        public bool Equals(Guid other) => other._guid == _guid;

        private System.Guid _guid;
    }
}
