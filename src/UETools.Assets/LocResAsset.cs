using System;
using System.CodeDom.Compiler;
using System.IO;
using UETools.Assets.Enums;
using UETools.Assets.Internal.Localization;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets
{
    [UnrealAssetFile(".locres")]
    public sealed class LocResAsset : IUnrealSerializable, IUnrealReadable, IUnrealLocalizationProvider
    {
        static Guid LocResMagic = new Guid(0x7574140E, 0x4A67, 0xFC03, 0x4A, 0x15, 0x90, 0x9D, 0xC3, 0x37, 0x7F, 0x1B);

        public FArchive Serialize(FArchive reader)
        {
            ReadVersion(reader);
            return reader.Read(ref _localizationTable);
        }

        private ELocResVersion ReadVersion(FArchive reader)
        {
            Guid MagicNumber = default;
            reader.Read(ref MagicNumber);
            var VersionNumber = ELocResVersion.Legacy;
            if (MagicNumber == LocResMagic)
                reader.ReadUnsafe(ref VersionNumber);
            else
                reader.Seek(0); // legacy LocRes

            reader.AssetVersion = (int)VersionNumber;
            return VersionNumber;
        }

        public void ReadTo(IndentedTextWriter writer)
        {
            if (_localizationTable is null)
                NotDeserializedException.Throw();

            _localizationTable.ReadTo(writer);
        }

        public string? Get(string key, string id) => _localizationTable.Get(key, id)?.LocalizedString?.ToString();

        public static LocResAsset English { get; } = new LocResAsset();

        LocalizationTable _localizationTable = null!;
    }
}
