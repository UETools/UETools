using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
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
