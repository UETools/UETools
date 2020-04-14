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
        public void Deserialize(FArchive reader)
        {
            foreach (var tag in PropertyTag.ReadToEnd(reader))
            {
                // TODO: Implement overriding processing?
                if (tag.Name.ToString() == "BindingIdToReferences")
                {
                    reader.Read(out int x);
                    reader.Read(out BindingIdToReferences);
                }
            }
        }

        Dictionary<Guid, TaggedObject> BindingIdToReferences = null!;
    }
}
