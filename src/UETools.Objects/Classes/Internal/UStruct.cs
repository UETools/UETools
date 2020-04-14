using System.Linq;
using UETools.Core;
using UETools.Objects.KismetVM;
using UETools.Objects.Package;

namespace UETools.Objects.Classes.Internal
{
    internal class UStruct : UField
    {
        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _superStruct);

            reader.Read(out int _childCount);
            for (var i = 0; i < _childCount; i++)
            {
                reader.Read(out ObjectReference ChildRef);
            }

            reader.Read(out _scriptByteCodeSize);
            reader.Read(out _scriptByteCodeOnDiskSize);
            if (_scriptByteCodeSize > 0)
            {
                var xxx = new BlueprintReader();
                xxx.Deserialize(reader);
            }
        }

        private ResolvedObjectReference<UStruct> _superStruct = null!;
        private ResolvedObjectReference<UField> _children = null!;
        private int _scriptByteCodeSize;
        private int _scriptByteCodeOnDiskSize;
    }
}
