using System;
using System.CodeDom.Compiler;
using System.IO;
using UnrealTools.Assets.Enums;
using UnrealTools.Assets.Internal.Localization;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Assets
{
    [UnrealAssetFile(".locres")]
    public sealed class LocResAsset : IUnrealDeserializable, IUnrealReadable, IUnrealLocalizationProvider
    {
        static Guid LocResMagic = new Guid(0x7574140E, 0x4A67, 0xFC03, 0x4A, 0x15, 0x90, 0x9D, 0xC3, 0x37, 0x7F, 0x1B);

        public void Deserialize(FArchive reader)
        {
            ReadVersion(reader);
            reader.Read(out _localizationTable);
        }

        private ELocResVersion ReadVersion(FArchive reader)
        {
            reader.Read(out Guid MagicNumber);
            var VersionNumber = ELocResVersion.Legacy;
            if (MagicNumber == LocResMagic)
                VersionNumber = reader.ReadUnsafe(out ELocResVersion _);
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
