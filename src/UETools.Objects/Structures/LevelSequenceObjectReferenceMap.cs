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
            public void Deserialize(FArchive reader)
            {
                reader.Read(out _objectId);
                reader.Read(out _objectPath);
            }

            Guid _objectId;
            FString _objectPath;
        }
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _map);
        }

        Dictionary<Guid, LevelSequenceLegacyObjectReference> _map;
    }
}
