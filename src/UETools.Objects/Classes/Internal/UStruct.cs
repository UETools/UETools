using System.Linq;
using UETools.Core;
using UETools.Objects.KismetVM;
using UETools.Objects.Package;

namespace UETools.Objects.Classes.Internal
{
    internal class UStruct : UField
    {
        public override FArchive Serialize(FArchive reader)
        {
            base.Serialize(reader)
                .Read(ref _superStruct);

            int _childCount = 0;
            reader.Read(ref _childCount);
            for (var i = 0; i < _childCount; i++)
            {
                ObjectReference? ChildRef = default;
                reader.Read(ref ChildRef);
            }

            reader.Read(ref _scriptByteCodeSize)
                  .Read(ref _scriptByteCodeOnDiskSize);
            if (_scriptByteCodeSize > 0)
            {
                var xxx = new BlueprintReader();
                xxx.Serialize(reader);
            }
            return reader;
        }

        private ResolvedObjectReference<UStruct>? _superStruct;
        private ResolvedObjectReference<UField>? _children;
        private int _scriptByteCodeSize;
        private int _scriptByteCodeOnDiskSize;
    }
}
