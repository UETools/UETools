using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    class LevelSequenceObjectReferenceMap : IUnrealStruct
    {
        struct LevelSequenceLegacyObjectReference : IUnrealStruct
        {
            public FArchive Serialize(FArchive archive)
                => archive.Read(ref _objectId)
                          .Read(ref _objectPath);

            Guid _objectId;
            FString _objectPath;
        }
        public FArchive Serialize(FArchive archive) => archive.Read(ref _map);

        Dictionary<Guid, LevelSequenceLegacyObjectReference>? _map;
    }
}
