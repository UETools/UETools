using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Objects.KismetVM
{
    public sealed class BlueprintReader : IUnrealSerializable
    {
        public TokenList Code { get; } = new TokenList();
        public long CodeLength => _codeEnd - _codeStart;

        public FArchive Serialize(FArchive archive)
        {
            _codeStart = archive.Tell();
            Code.ReadUntil(archive, EExprToken.EX_EndOfScript);
            _codeEnd = archive.Tell();
            return archive;
        }

        private long _codeStart;
        private long _codeEnd;
    }
}
