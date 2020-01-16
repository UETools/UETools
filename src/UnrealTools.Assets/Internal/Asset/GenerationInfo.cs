using UnrealTools.Core;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Assets.Internal.Asset
{
    internal struct GenerationInfo : IUnrealDeserializable
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _exportCount);
            reader.Read(out _nameCount);
        }

        private int _exportCount;
        private int _nameCount;
    }
}
