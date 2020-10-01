#if NETSTANDARD2_0
using CoreExtensions;
#endif
using System.Collections.Generic;
using System.Linq;
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
            .AppendJoin(", ", _tags.AsEnumerable())
            .Append(" }")
            .ToString();

        List<FName> _tags;
    }
}
