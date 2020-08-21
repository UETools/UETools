using System.Diagnostics.CodeAnalysis;
using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    public struct Guid : IUnrealStruct, System.IEquatable<Guid>, System.IEquatable<System.Guid>
    {
        public FArchive Serialize(FArchive reader) => reader.Read(ref _guid);

        public override string ToString() => _guid.ToString();

        public bool Equals(Guid other) => other._guid == _guid;

        public bool Equals([AllowNull] System.Guid other) => _guid == other;

        private System.Guid _guid;
    }
}
