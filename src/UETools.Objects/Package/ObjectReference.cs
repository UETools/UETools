using System.Diagnostics;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Objects.Package
{
    [DebuggerDisplay("{Resource?.FullName}")]
    public class ObjectReference : IUnrealDeserializable
    {
        public ObjectResource? Resource { get; private set; }

        public virtual void Deserialize(FArchive reader)
        {
            reader.Read(out _objectIndex);
            Resource = _objectIndex.Resolve(reader);
        }

        public override string ToString() => Resource is null ? "null" : Resource.FullName;

        private PackageIndex _objectIndex;
    }
}
