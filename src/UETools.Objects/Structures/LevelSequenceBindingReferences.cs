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
        public FArchive Serialize(FArchive reader)
        {
            foreach (var tag in PropertyTag.ReadToEnd(reader))
            {
                // TODO: Implement overriding processing?
                if (tag.Name.ToString() == "BindingIdToReferences")
                {
                    int x = 0;
                    reader.Read(ref x);
                    reader.Read(ref BindingIdToReferences);
                }
            }
            return reader;
        }

        Dictionary<Guid, TaggedObject>? BindingIdToReferences;
    }
}
