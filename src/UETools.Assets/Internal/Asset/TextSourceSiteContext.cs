using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Asset
{
    internal class TextSourceSiteContext : IUnrealSerializable
    {
        public FArchive Serialize(FArchive reader) 
            => reader.Read(ref _keyName)
                     .Read(ref _siteDescription)
                     .Read(ref _isEditorOnly)
                     .Read(ref _isOptional)
                     .Read(ref _infoMetaData)
                     .Read(ref _keyMetaData);

        private bool _isOptional;
        private bool _isEditorOnly;
        private FString _keyName = null!;
        private FString _siteDescription = null!;
        private LocMetadataObject _infoMetaData = null!;
        private LocMetadataObject _keyMetaData = null!;
    }
}