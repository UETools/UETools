using System;
using System.IO;
using UETools.Core;
using UETools.Core.Enums;
using UETools.Core.Interfaces;
using UETools.Objects.Classes;
using UETools.Objects.Enums;

namespace UETools.Objects.Package
{
    public partial class ObjectExport : ObjectResource, IUnrealSerializable
    {
        public ObjectResource? DefaultObject { get; private set; }
        public ObjectResource? Class { get; private set; }
        public ObjectResource? Super { get; private set; }
        public bool IsDefaultObject { get; private set; }
        public bool IsArchetypeObject { get; private set; }
        public TaggedObject? Object => _deserialized;
        internal EObjectFlags ObjectFlags => _objectFlags;
        internal bool IsAsset => _bIsAsset;
        internal bool WasPrinted { get; set; }

        public FArchive Serialize(FArchive reader)
        {
            var version = reader.Version;

            reader.Read(ref _classIndex)
                  .Read(ref _superIndex);

            if (version >= UE4Version.VER_UE4_TemplateIndex_IN_COOKED_EXPORTS)
                reader.Read(ref _templateIndex);

            reader.Read(ref _outerIndex)
                  .Read(ref _objectName)
                  .ReadUnsafe(ref _objectFlags);
            IsDefaultObject = (_objectFlags & EObjectFlags.RF_Standalone) != 0 && (_objectFlags & EObjectFlags.RF_Public) != 0;
            IsArchetypeObject = (_objectFlags & EObjectFlags.RF_ArchetypeObject) != 0;

            if (version < UE4Version.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES)
            {
                int size = 0, offset = 0;
                reader.Read(ref size)
                      .Read(ref offset);
                _serialSize = size;
                _serialOffset = offset;
            }
            else
            {
                reader.Read(ref _serialSize)
                      .Read(ref _serialOffset);
            }

            reader.Read(ref _bForcedExport)
                  .Read(ref _bNotForClient)
                  .Read(ref _bNotForServer)
                  .Read(ref _packageGuid)
                  .ReadUnsafe(ref _packageFlags);

            if (version >= UE4Version.VER_UE4_LOAD_FOR_EDITOR_GAME)
                reader.Read(ref _bNotAlwaysLoadedForEditorGame);

            if (version >= UE4Version.VER_UE4_COOKED_ASSETS_IN_EDITOR_SUPPORT)
                reader.Read(ref _bIsAsset);

            if (version >= UE4Version.VER_UE4_PRELOAD_DEPENDENCIES_IN_COOKED_EXPORTS)
            {
                reader.Read(ref _firstExportDependency)
                      .Read(ref _serializationBeforeSerializationDependencies)
                      .Read(ref _createBeforeSerializationDependencies)
                      .Read(ref _serializationBeforeCreateDependencies)
                      .Read(ref _createBeforeCreateDependencies);
            }

            return reader;
        }

        public override void Fix(FArchive reader)
        {
            Class = reader.ImpExp(_classIndex);
            if (Class is null)
                _fullNameBuilder.Append("<Unknown Class>");
            else
                _fullNameBuilder.Append(GetClassName());

            _fullNameBuilder.Append(" ");
            Outer = reader.ImpExp(_outerIndex);
            GetOuter(Outer, reader);
            _fullNameBuilder.Append(_objectName);

            Super = reader.ImpExp(_superIndex);
            DefaultObject = reader.ImpExp(_templateIndex);

             _deserialized = Read(reader);
        }
        public override TaggedObject? Read(FArchive? reader)
        {
            if (_deserialized is null && reader is { })
            {
                _deserialized = TaggedObject.Create(reader.Slice(_serialOffset), GetClassName());
            }

            return _deserialized;
        }
        public override string GetClassName() => Class is null ? string.Empty : Class.ObjectName.Name.Value;

        private PackageIndex _classIndex;
        private PackageIndex _superIndex;
        private PackageIndex _templateIndex;
        private EObjectFlags _objectFlags;
        private long _serialSize;
        private long _serialOffset;
        private bool _bForcedExport;
        private bool _bNotForClient;
        private bool _bNotForServer;
        private Guid _packageGuid;
        private EPackageFlags _packageFlags;
        private bool _bNotAlwaysLoadedForEditorGame;
        private bool _bIsAsset;
        private int _firstExportDependency;
        private int _serializationBeforeSerializationDependencies;
        private int _createBeforeSerializationDependencies;
        private int _serializationBeforeCreateDependencies;
        private int _createBeforeCreateDependencies;

        private TaggedObject? _deserialized;
    }
}
