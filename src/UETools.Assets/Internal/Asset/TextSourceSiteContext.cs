using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Asset
{
    internal class TextSourceSiteContext : IUnrealDeserializable
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _keyName);
            reader.Read(out _siteDescription);
            reader.Read(out _isEditorOnly);
            reader.Read(out _isOptional);
            reader.Read(out _infoMetaData);
            reader.Read(out _keyMetaData);
        }

        private bool _isOptional;
        private bool _isEditorOnly;
        private FString _keyName = null!;
        private FString _siteDescription = null!;
        LocMetadataObject _infoMetaData = null!;
        LocMetadataObject _keyMetaData = null!;
    }
}