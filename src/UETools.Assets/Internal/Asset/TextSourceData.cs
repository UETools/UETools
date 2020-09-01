using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Asset
{
    internal class TextSourceData : IUnrealSerializable
    {
        public FArchive Serialize(FArchive archive)
            => archive.Read(ref _sourceString)
                      .Read(ref _sourceStringMetaData);

        private FString _sourceString = null!;
        private LocMetadataObject _sourceStringMetaData = null!;
    }
}