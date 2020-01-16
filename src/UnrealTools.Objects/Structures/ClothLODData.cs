using System.Collections.Generic;
using System.Text;
using UnrealTools.Core;
using UnrealTools.Objects.Classes;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    sealed class ClothLODData : TaggedObject, IUnrealStruct
    {
        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _transitionUpSkinData);
            reader.Read(out _transitionDownSkinData);
        }

        private List<MeshToMeshVertData> _transitionUpSkinData = null!;
        private List<MeshToMeshVertData> _transitionDownSkinData = null!;
    }
}
