using System.Diagnostics;
using UETools.Core.Enums;
using UETools.Core.Interfaces;

namespace UETools.Core.HistoryTypes
{
    [DebuggerDisplay("{_argumentName.ToString()}: {_argumentValue.ToString()}")]
    class FFormatArgumentData : IUnrealSerializable
    {
        public FArchive Serialize(FArchive reader)
        {
            if (reader.Version >= UE4Version.VER_UE4_K2NODE_VAR_REFERENCEGUIDS)
            {
                FString? _arg = default;
                reader.Read(ref _arg);
                _argumentName = FText.FromString(_arg);
            }
            else
                reader.Read(ref _argumentName);

            return reader.Read(ref _argumentValue);
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