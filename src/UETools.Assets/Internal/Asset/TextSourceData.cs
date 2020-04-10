using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Asset
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