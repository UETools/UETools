using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Classes;

namespace UETools.Objects.Package
{
    public sealed class ObjectImport : ObjectResource, IUnrealSerializable
    {
        public FArchive Serialize(FArchive archive)
            => archive.Read(ref _classPackage)
                      .Read(ref _className)
                      .Read(ref _outerIndex)
                      .Read(ref _objectName);

        public override void Fix(FArchive reader)
        {
            _fullNameBuilder.Append(_className);
            _fullNameBuilder.Append(" ");
            Outer = _outerIndex.Resolve(reader);
            GetOuter(Outer, reader);
            _fullNameBuilder.Append(ObjectName);
        }
        public override TaggedObject? Read(FArchive? reader) => null;
        public override string GetClassName() => _className;


        private FName _classPackage = null!;
        private FName _className = null!;
    }
}