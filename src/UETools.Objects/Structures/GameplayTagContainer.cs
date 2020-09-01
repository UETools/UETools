using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    struct GameplayTagContainer : IUnrealStruct
    {
        public FArchive Serialize(FArchive archive) => archive.Read(ref _tags);

        public override string ToString() => new StringBuilder()
            .Append("{ ")
            .AppendJoin(", ", _tags)
            .Append(" }")
            .ToString();

        List<FName> _tags;
    }
}
