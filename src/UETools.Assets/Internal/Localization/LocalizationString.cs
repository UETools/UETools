using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Localization
{
    internal class LocalizationString : IUnrealSerializable
    {
        public FString Value => _value;
        public LocalizationString() : this(null!) { }
        public LocalizationString(FString value) => _value = value;

        public FArchive Serialize(FArchive archive)
            => archive.Read(ref _value)
                      .Read(ref _refCount);

        private FString _value;
        private int _refCount;
    }
}