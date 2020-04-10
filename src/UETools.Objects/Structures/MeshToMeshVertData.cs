using System;
using System.Collections.Generic;
using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    struct MeshToMeshVertData : IUnrealStruct
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _positionBaryCordsAndDist);
            reader.Read(out _normalBaryCordsAndDist);
            reader.Read(out _tangentBaryCordsAndDist);
            reader.Read(out _sourceMeshVertIndices, 4);
            reader.Read(out _padding, 2);
        }
        Vector4 _positionBaryCordsAndDist;
        Vector4 _normalBaryCordsAndDist;
        Vector4 _tangentBaryCordsAndDist;
        List<ushort> _sourceMeshVertIndices;
        List<uint> _padding;
    }
}
