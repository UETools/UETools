using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Objects.Classes;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    class LevelSequenceBindingReferences : IUnrealStruct
    {
        public FArchive Serialize(FArchive archive)
        {
            foreach (var tag in PropertyTag.ReadToEnd(archive))
            {
                // TODO: Implement overriding processing?
                if (tag.Name.ToString() == "BindingIdToReferences")
                {
                    int x = 0;
                    archive.Read(ref x);
                    archive.Read(ref BindingIdToReferences);
                }
            }
            return archive;
        }

        Dictionary<Guid, TaggedObject>? BindingIdToReferences;
    }
}
