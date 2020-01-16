using UnrealTools.Core;
using UnrealTools.Core.Interfaces;
using UnrealTools.Objects.Classes;

namespace UnrealTools.Objects.Package
{
    public sealed class ObjectImport : ObjectResource, IUnrealDeserializable
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _classPackage);
            reader.Read(out _className);
            reader.Read(out _outerIndex);
            reader.Read(out _objectName);
        }

        public override void Fix(FArchive reader)
        {
            _fullNameBuilder.Append(_className);
            _fullNameBuilder.Append(" ");
            Outer = _outerIndex.Resolve(reader);
            GetOuter(Outer, reader);
            _fullNameBuilder.Append(ObjectName);
        }
        public override TaggedObject? Read(FArchive reader) => null;
        public override string GetClassName() => _className;


        private FName _classPackage = null!;
        private FName _className = null!;
    }
}