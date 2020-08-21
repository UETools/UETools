using UETools.Core;
using UETools.Core.Enums;
using UETools.Core.Interfaces;

namespace UETools.Objects.KismetVM
{
    public struct CodeSkipSize : IUnrealSerializable
    {
        public static implicit operator int(CodeSkipSize size) => size.SkipValue;
        public static implicit operator CodeSkipSize(int size) => new CodeSkipSize()
        {
            SkipValue = size
        };

        public int SkipValue { get; private set; }

        public FArchive Serialize(FArchive archive)
        {
            if (archive.Version < UE4Version.VER_UE4_DISABLED_SCRIPT_LIMIT_BYTECODE)
            {
                short skip = 0;
                archive.Read(ref skip);
                SkipValue = skip;
            }
            else
            {
                int skip = 0;
                archive.Read(ref skip);
                SkipValue = skip;
            }
            return archive;
        }

        public override string ToString() => SkipValue.ToString();
    }
}
