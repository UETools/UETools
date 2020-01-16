using UnrealTools.Core;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Assets.Internal.Asset
{
    internal class TextSourceData : IUnrealDeserializable
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _sourceString);
            reader.Read(out _sourceStringMetaData);
        }

        private FString _sourceString = null!;
        private LocMetadataObject _sourceStringMetaData = null!;
    }
}