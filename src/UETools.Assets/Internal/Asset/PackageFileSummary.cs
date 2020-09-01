using System;
using System.Collections.Generic;
using UETools.Core;
using UETools.Core.Enums;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Asset
{
    internal partial class PackageFileSummary : IUnrealSerializable
    {
        private const long PACKAGE_TAG_MAGIC = 0x9E2A83C1;
        private const long PACKAGE_TAG_MAGIC_SWAPPED = 0xC1832A9E;

        public CustomVersionContainer CustomVersionContainer { get; private set; } = null!;

        public bool IsUnversioned { get; private set; }
        public bool IsAssetFile => _tag == PACKAGE_TAG_MAGIC;

        public int NameCount => _nameCount;
        public int NameOffset => _nameOffset;
        public int GatherableTextDataCount => _gatherableTextDataCount;
        public int GatherableTextDataOffset => _gatherableTextDataOffset;
        public int ExportCount => _exportCount;
        public int ExportOffset => _exportOffset;
        public int ImportCount => _importCount;
        public int ImportOffset => _importOffset;
        public int DependsOffset => _dependsOffset;
        public int SoftPackageReferencesCount => _softPackageReferencesCount;
        public int SoftPackageReferencesOffset => _softPackageReferencesOffset;

        public FArchive Serialize(FArchive archive)
        {
            const long MinimumPackageSize = 32;
            if (archive.Length() < MinimumPackageSize)
                return archive;

            archive.Read(ref _tag);
            if (!IsAssetFile)
                return archive;

            const int CurrentLegacyFileVersion = -7;
            int LegacyFileVersion = 0;
            archive.Read(ref LegacyFileVersion);
            if (LegacyFileVersion < 0)
            {
                if (LegacyFileVersion < CurrentLegacyFileVersion)
                {
                    _fileVersionUE4 = 0;
                    _fileVersionLicenseeUE4 = 0;
                    return archive;
                }

                if (LegacyFileVersion != -4)
                {
                    int LegacyUE3Version = 0;
                    // skip legacy ue3 version
                    archive.Read(ref LegacyUE3Version);
                }

                archive.ReadUnsafe(ref _fileVersionUE4);
                archive.Read(ref _fileVersionLicenseeUE4);

                if (LegacyFileVersion <= -2)
                {
                    CustomVersionContainer = new CustomVersionContainer(CustomVersionFormatForArchive(LegacyFileVersion));
                    CustomVersionContainer.Serialize(archive);
                }

                if (_fileVersionUE4 == 0 && _fileVersionLicenseeUE4 == 0)
                {
                    IsUnversioned = true;
                    if (archive.Version == 0)
                    {
                        // Set latest
                        archive.Version = _fileVersionUE4 = UE4Version.VER_UE4_AUTOMATIC_VERSION;
                        _fileVersionLicenseeUE4 = 0;
                    }
                    else
                        _fileVersionUE4 = archive.Version; // version read from config file
                }
            }
            else
            {
                _fileVersionUE4 = 0;
                _fileVersionLicenseeUE4 = 0;
            }

            archive.Read(ref _totalHeaderSize)
                   .Read(ref _folderName)
                   .ReadUnsafe(ref _packageFlags)
                   .Read(ref _nameCount)
                   .Read(ref _nameOffset);

            if ((_packageFlags & EPackageFlags.FilterEditorOnly) == 0 && _fileVersionUE4 >= UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)
                archive.Read(ref _localizationId);

            if (_fileVersionUE4 >= UE4Version.VER_UE4_SERIALIZE_TEXT_IN_PACKAGES)
            {
                archive.Read(ref _gatherableTextDataCount)
                       .Read(ref _gatherableTextDataOffset);
            }

            archive.Read(ref _exportCount)
                   .Read(ref _exportOffset)
                   .Read(ref _importCount)
                   .Read(ref _importOffset)
                   .Read(ref _dependsOffset);

            if (_fileVersionUE4 < UE4Version.VER_UE4_OLDEST_LOADABLE_PACKAGE)
                return archive;

            if (_fileVersionUE4 >= UE4Version.VER_UE4_ADD_STRING_ASSET_REFERENCES_MAP)
            {
                archive.Read(ref _softPackageReferencesCount)
                       .Read(ref _softPackageReferencesOffset);
            }

            if (_fileVersionUE4 >= UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)
                archive.Read(ref _searchableNamesOffset);

            archive.Read(ref _thumbnailTableOffset)
                   .Read(ref _guid)
                   .Read(ref _generations);

            if (_fileVersionUE4 >= UE4Version.VER_UE4_ENGINE_VERSION_OBJECT)
                archive.Read(ref _savedByEngineVersion);
            else
            {
                uint changelist = 0;
                archive.Read(ref changelist);
                _savedByEngineVersion = new EngineVersion(4, 0, 0, changelist, string.Empty);
            }

            if (_fileVersionUE4 >= UE4Version.VER_UE4_PACKAGE_SUMMARY_HAS_COMPATIBLE_ENGINE_VERSION)
                archive.Read(ref _compatibleWithEngineVersion);
            else
                _compatibleWithEngineVersion = _savedByEngineVersion;

            archive.Read(ref _compressionFlags)
                   .Read(ref _compressedChunks);
            if (_compressedChunks.Count > 0)
                throw new NotSupportedException("Package Level Compression is not supported by UE4 anymore.");

            archive.Read(ref _packageSource);

            // No longer used: List of additional packages that are needed to be cooked for this package (ie streaming levels)
            // Keeping the serialization code for backwards compatibility without bumping the package version
            List<FString>? _additionalPackagesToCook = default;
            archive.Read(ref _additionalPackagesToCook);

            if (LegacyFileVersion > -7)
            {
                // Texture allocations no longer supported.
                int NumTextureAllocations = 0;
                archive.Read(ref NumTextureAllocations);
            }

            archive.Read(ref _assetRegistryDataOffset)
                   .Read(ref _bulkDataStartOffset);

            if (_fileVersionUE4 >= UE4Version.VER_UE4_WORLD_LEVEL_INFO)
                archive.Read(ref _worldTileInfoDataOffset);

            if (_fileVersionUE4 >= UE4Version.VER_UE4_CHANGED_CHUNKID_TO_BE_AN_ARRAY_OF_CHUNKIDS)
                archive.Read(ref _chunkIDs);
            else if (_fileVersionUE4 >= UE4Version.VER_UE4_ADDED_CHUNKID_TO_ASSETDATA_AND_UPACKAGE)
                archive.Read(ref _chunkIDs, 1);

            if (_fileVersionUE4 >= UE4Version.VER_UE4_PRELOAD_DEPENDENCIES_IN_COOKED_EXPORTS)
            {
                archive.Read(ref _preloadDependencyCount)
                       .Read(ref _preloadDependencyOffset);
            }

            return archive;
        }

        private CustomVersionSerializationFormat CustomVersionFormatForArchive(int LegacyVersion)
        {
            var CustomVersionFormat = CustomVersionSerializationFormat.Unknown;
            if (LegacyVersion == -2)
            {
                CustomVersionFormat = CustomVersionSerializationFormat.Enums;
            }
            else if (LegacyVersion < -2 && LegacyVersion >= -5)
            {
                CustomVersionFormat = CustomVersionSerializationFormat.Guids;
            }
            else if (LegacyVersion < -5)
            {
                CustomVersionFormat = CustomVersionSerializationFormat.Optimized;
            }

            if (CustomVersionFormat == CustomVersionSerializationFormat.Unknown)
                throw new UnrealException("Unknown CustomVersionFormat encountered.");

            return CustomVersionFormat;
        }

        private uint _tag;
        private UE4Version _fileVersionUE4;
        private int _fileVersionLicenseeUE4;
        private int _totalHeaderSize;
        private EPackageFlags _packageFlags;
        private FString _folderName = null!;
        private int _nameCount;
        private int _nameOffset;
        private int _gatherableTextDataCount;
        private int _gatherableTextDataOffset;
        private int _exportCount;
        private int _exportOffset;
        private int _importCount;
        private int _importOffset;
        private int _dependsOffset;
        private int _softPackageReferencesCount;
        private int _softPackageReferencesOffset;
        private int _searchableNamesOffset;
        private int _thumbnailTableOffset;
        private Guid _guid;
        private List<GenerationInfo> _generations = null!;
        private EngineVersion _savedByEngineVersion = null!;
        private EngineVersion _compatibleWithEngineVersion = null!;
        private uint _compressionFlags;
        private List<CompressedChunk> _compressedChunks = null!;
        private uint _packageSource;
        private int _assetRegistryDataOffset;
        private long _bulkDataStartOffset;
        private int _worldTileInfoDataOffset;
        private List<int> _chunkIDs = null!;
        private int _preloadDependencyCount;
        private int _preloadDependencyOffset;

        private FString _localizationId = null!;
    }
}
