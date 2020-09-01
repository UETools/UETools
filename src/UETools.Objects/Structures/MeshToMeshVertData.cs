using System;
using System.Collections.Generic;
using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    struct MeshToMeshVertData : IUnrealStruct
    {
        public FArchive Serialize(FArchive archive)
            => archive.Read(ref _positionBaryCordsAndDist)
                      .Read(ref _normalBaryCordsAndDist)
                      .Read(ref _tangentBaryCordsAndDist)
                      .Read(ref _sourceMeshVertIndices, 4)
                      .Read(ref _padding, 2);

        Vector4 _positionBaryCordsAndDist;
        Vector4 _normalBaryCordsAndDist;
        Vector4 _tangentBaryCordsAndDist;
        List<ushort> _sourceMeshVertIndices;
        List<uint> _padding;
    }
}
