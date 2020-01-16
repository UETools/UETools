using System;
using System.Collections.Generic;
using UnrealTools.Core;
using UnrealTools.Core.Enums;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Assets.Internal.Asset
{
    internal partial class PackageFileSummary : IUnrealDeserializable
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

        public void Deserialize(FArchive reader)
        {
            const long MinimumPackageSize = 32;
            if (reader.Length() < MinimumPackageSize)
                return;

            reader.Read(out _tag);
            if (!IsAssetFile)
                return;

            const int CurrentLegacyFileVersion = -7;
            reader.Read(out int LegacyFileVersion);
            if (LegacyFileVersion < 0)
            {
                if (LegacyFileVersion < CurrentLegacyFileVersion)
                {
                    _fileVersionUE4 = 0;
                    _fileVersionLicenseeUE4 = 0;
                    return;
                }

                if (LegacyFileVersion != -4)
                {
                    // skip legacy ue3 version
                    reader.Read(out int LegacyUE3Version);
                }

                reader.ReadUnsafe(out _fileVersionUE4);
                reader.Read(out _fileVersionLicenseeUE4);

                if (LegacyFileVersion <= -2)
                {
                    CustomVersionContainer = new CustomVersionContainer(CustomVersionFormatForArchive(LegacyFileVersion));
                    CustomVersionContainer.Deserialize(reader);
                }

                if (_fileVersionUE4 == 0 && _fileVersionLicenseeUE4 == 0)
                {
                    IsUnversioned = true;
                    if (reader.Version == 0)
                    {
                        // Set latest
                        reader.Version = _fileVersionUE4 = UE4Version.VER_UE4_AUTOMATIC_VERSION;
                        _fileVersionLicenseeUE4 = 0;
                    }
                    else
                        _fileVersionUE4 = reader.Version; // version read from config file
                }
            }
            else
            {
                _fileVersionUE4 = 0;
                _fileVersionLicenseeUE4 = 0;
            }

            reader.Read(out _totalHeaderSize);
            reader.Read(out _folderName);
            reader.ReadUnsafe(out _packageFlags);

            reader.Read(out _nameCount);
            reader.Read(out _nameOffset);

            if ((_packageFlags & EPackageFlags.FilterEditorOnly) == 0 && _fileVersionUE4 >= UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)
                reader.Read(out _localizationId);

            if (_fileVersionUE4 >= UE4Version.VER_UE4_SERIALIZE_TEXT_IN_PACKAGES)
            {
                reader.Read(out _gatherableTextDataCount);
                reader.Read(out _gatherableTextDataOffset);
            }

            reader.Read(out _exportCount);
            reader.Read(out _exportOffset);
            reader.Read(out _importCount);
            reader.Read(out _importOffset);
            reader.Read(out _dependsOffset);

            if (_fileVersionUE4 < UE4Version.VER_UE4_OLDEST_LOADABLE_PACKAGE)
                return;

            if (_fileVersionUE4 >= UE4Version.VER_UE4_ADD_STRING_ASSET_REFERENCES_MAP)
            {
                reader.Read(out _softPackageReferencesCount);
                reader.Read(out _softPackageReferencesOffset);
            }

            if (_fileVersionUE4 >= UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)
                reader.Read(out _searchableNamesOffset);

            reader.Read(out _thumbnailTableOffset);
            reader.Read(out _guid);

            reader.Read(out _generations);

            if (_fileVersionUE4 >= UE4Version.VER_UE4_ENGINE_VERSION_OBJECT)
                reader.Read(out _savedByEngineVersion);
            else
            {
                reader.Read(out uint changelist);
                _savedByEngineVersion = new EngineVersion(4, 0, 0, changelist, string.Empty);
            }

            if (_fileVersionUE4 >= UE4Version.VER_UE4_PACKAGE_SUMMARY_HAS_COMPATIBLE_ENGINE_VERSION)
                reader.Read(out _compatibleWithEngineVersion);
            else
                _compatibleWithEngineVersion = _savedByEngineVersion;

            reader.Read(out _compressionFlags);

            reader.Read(out _compressedChunks);
            if (_compressedChunks.Count > 0)
                throw new NotSupportedException("Package Level Compression is not supported by UE4 anymore.");

            reader.Read(out _packageSource);

            // No longer used: List of additional packages that are needed to be cooked for this package (ie streaming levels)
            // Keeping the serialization code for backwards compatibility without bumping the package version
            List<FString> _additionalPackagesToCook;
            reader.Read(out _additionalPackagesToCook);

            if (LegacyFileVersion > -7)
            {
                // Texture allocations no longer supported.
                reader.Read(out int NumTextureAllocations);
            }

            reader.Read(out _assetRegistryDataOffset);
            reader.Read(out _bulkDataStartOffset);

            if (_fileVersionUE4 >= UE4Version.VER_UE4_WORLD_LEVEL_INFO)
                reader.Read(out _worldTileInfoDataOffset);

            if (_fileVersionUE4 >= UE4Version.VER_UE4_CHANGED_CHUNKID_TO_BE_AN_ARRAY_OF_CHUNKIDS)
                reader.Read(out _chunkIDs);
            else if (_fileVersionUE4 >= UE4Version.VER_UE4_ADDED_CHUNKID_TO_ASSETDATA_AND_UPACKAGE)
                reader.Read(out _chunkIDs, 1);

            if (_fileVersionUE4 >= UE4Version.VER_UE4_PRELOAD_DEPENDENCIES_IN_COOKED_EXPORTS)
            {
                reader.Read(out _preloadDependencyCount);
                reader.Read(out _preloadDependencyOffset);
            }
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
