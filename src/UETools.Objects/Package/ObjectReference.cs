using System.Diagnostics;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Objects.Package
{
    [DebuggerDisplay("{Resource?.FullName}")]
    public class ObjectReference : IUnrealSerializable
    {
        public ObjectResource? Resource { get; private set; }

        public virtual FArchive Serialize(FArchive reader)
        {
            reader.Read(ref _objectIndex);
            Resource = _objectIndex.Resolve(reader);
            return reader;
        }

        public override string ToString() => Resource is null ? "null" : Resource.FullName;

        private PackageIndex _objectIndex;
    }
}
