using System;
using System.Collections.Generic;
using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Classes;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    struct SkeletalMeshSamplingLODBuiltData : IUnrealStruct
    {
        private class WeighterRandomSampler : IUnrealSerializable
        {
            public FArchive Serialize(FArchive reader)
                => reader.Read(ref _prob)
                         .Read(ref _alias)
                         .Read(ref _totalWeight);

            List<float> _prob = null!;
            List<int> _alias = null!;
            float _totalWeight;
        }
        private class SkeletalMeshAreaWeightedTriangleSampler : WeighterRandomSampler
        {

        }

        public FArchive Serialize(FArchive reader) => reader.Read(ref _skeletalMeshArea);

        private SkeletalMeshAreaWeightedTriangleSampler _skeletalMeshArea;
    }
}
