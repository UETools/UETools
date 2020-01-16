using UnrealTools.Core;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.KismetVM
{
    public sealed class BlueprintReader : IUnrealDeserializable
    {
        public TokenList Code { get; } = new TokenList();
        public long CodeLength => _codeEnd - _codeStart;

        public void Deserialize(FArchive reader)
        {
            _codeStart = reader.Tell();
            Code.ReadUntil(reader, EExprToken.EX_EndOfScript);
            _codeEnd = reader.Tell();
        }

        private long _codeStart;
        private long _codeEnd;
    }
}
