using System.Collections.Generic;
using System.Text;
using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    struct GameplayTagContainer : IUnrealStruct
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _tags);
        }

        public override string ToString() => new StringBuilder()
            .Append("{ ")
            .AppendJoin(", ", _tags)
            .Append(" }")
            .ToString();

        List<FName> _tags;
    }
}
