using System.Diagnostics;
using UnrealTools.Core.Enums;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Core.HistoryTypes
{
    [DebuggerDisplay("{_argumentName.ToString()}: {_argumentValue.ToString()}")]
    class FFormatArgumentData : IUnrealDeserializable
    {
        public void Deserialize(FArchive reader)
        {
            if (reader.Version >= UE4Version.VER_UE4_K2NODE_VAR_REFERENCEGUIDS)
            {
                reader.Read(out FString _arg);
                _argumentName = FText.FromString(_arg);
            }
            else
                reader.Read(out _argumentName);

            reader.Read(out _argumentValue);
        }

        public string Replace(string sourceValue)
        {
            if (_argumentName is null || _argumentValue is null)
                NotDeserializedException.Throw();

            return sourceValue.Replace(_argumentName.ToString(), _argumentValue.ToString());
        }

        private FText _argumentName = null!;
        private FFormatArgumentValue _argumentValue = null!;
    }
}