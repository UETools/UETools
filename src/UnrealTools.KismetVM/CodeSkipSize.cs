using UnrealTools.Core;
using UnrealTools.Core.Enums;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.KismetVM
{
    public struct CodeSkipSize : IUnrealDeserializable
    {
        public static implicit operator int(CodeSkipSize size) => size.SkipValue;
        public static implicit operator CodeSkipSize(int size) => new CodeSkipSize()
        {
            SkipValue = size
        };

        public int SkipValue { get; private set; }

        public void Deserialize(FArchive reader)
        {
            if (reader.Version < UE4Version.VER_UE4_DISABLED_SCRIPT_LIMIT_BYTECODE)
            {
                reader.Read(out short skip);
                SkipValue = skip;
            }
            else
            {
                reader.Read(out int skip);
                SkipValue = skip;
            }
        }

        public override string ToString() => SkipValue.ToString();
    }
}
