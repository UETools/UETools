using System.Diagnostics;
using System.Text;
using UETools.Core;
using UETools.Objects.Classes;

namespace UETools.Objects.Package
{
    [DebuggerDisplay("{FullName}")]
    public abstract class ObjectResource
    {
        public ObjectResource? Outer { get; protected set; }
        public FName ObjectName { get => _objectName; set => _objectName = value; }
        public string FullName => _fullNameBuilder.ToString();

        protected void GetOuter(ObjectResource? res, FArchive asset)
        {
            if (res is null)
                return;

            GetOuter(asset.ImpExp(res._outerIndex), asset);
            _fullNameBuilder.Append(res._objectName).Append(".");
        }

        public ObjectResource GetOutermost(FArchive asset) => asset.ImpExp(_outerIndex)?.GetOutermost(asset) ?? this;

        public abstract void Fix(FArchive reader);
        public abstract TaggedObject? Read(FArchive reader);
        public abstract string GetClassName();

        protected PackageIndex _outerIndex;
        protected FName _objectName = null!;
        protected StringBuilder _fullNameBuilder = new StringBuilder(256);
    }
}
