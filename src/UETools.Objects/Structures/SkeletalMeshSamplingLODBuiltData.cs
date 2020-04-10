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
        private class WeighterRandomSampler : IUnrealDeserializable
        {
            public void Deserialize(FArchive reader)
            {
                reader.Read(out _prob);
                reader.Read(out _alias);
                reader.Read(out _totalWeight);
            }

            List<float> _prob = null!;
            List<int> _alias = null!;
            float _totalWeight;
        }
        private class SkeletalMeshAreaWeightedTriangleSampler : WeighterRandomSampler
        {

        }

        public void Deserialize(FArchive reader)
        {
            reader.Read(out _skeletalMeshArea);
        }

        private SkeletalMeshAreaWeightedTriangleSampler _skeletalMeshArea;
    }
}
