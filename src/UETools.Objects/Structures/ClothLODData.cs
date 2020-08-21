using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Objects.Classes;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    class ClothLODDataCommon : ClothLODData 
    { 
    }
    class ClothLODData : TaggedObject, IUnrealStruct
    {
        public override FArchive Serialize(FArchive reader) 
            => base.Serialize(reader)
                   .Read(ref _transitionUpSkinData)
                   .Read(ref _transitionDownSkinData);

        private List<MeshToMeshVertData> _transitionUpSkinData = null!;
        private List<MeshToMeshVertData> _transitionDownSkinData = null!;
    }
}
