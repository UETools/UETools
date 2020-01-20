using System;
using System.Diagnostics;
using UnrealTools.Core.Enums;
using UnrealTools.Core.HistoryTypes;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Core
{
    [DebuggerDisplay("FText: {ToString()}")]
    public sealed class FText : IUnrealDeserializable
    {
        public FText() : this(null!) { }
        private FText(FTextHistory history) => _history = history;

        public void Deserialize(FArchive reader)
        {
            reader.ReadUnsafe(out _flags);
            if (reader.Version < UE4Version.VER_UE4_FTEXT_HISTORY)
            {
                reader.Read(out FString _value);
                if (reader.Version >= UE4Version.VER_UE4_ADDED_NAMESPACE_AND_KEY_DATA_TO_FTEXT)
                    _history = new FTextHistory.Base(_value);
                else
                {
                    reader.Read(out FString _namespace);
                    reader.Read(out FString _key);
                    _history = new FTextHistory.Base(_value, _namespace, _key);
                }
            }
            else
            {
                reader.ReadUnsafe(out TextHistoryType HistoryType);
                if (FTextHistory.HistoryTypes.TryGetValue(HistoryType, out var init))
                {
                    _history = init();
                    _history.Deserialize(reader);
                }
                else
                    Debug.WriteLine($"HistoryType unrecognized: {HistoryType}");
            }
        }
        public override string ToString()
        {
            if (_history is null)
                NotDeserializedException.Throw();

            return _history.ToString();
        }

        // TODO: Static methods for history types
        public static FText GetEmpty() => new FText();
        public static FText FromString(FString value)
        {
            var history = new FTextHistory.Base(value);
            return new FText(history);
        }
        public static FText FromStringTable()
        {
            var history = new FTextHistory.StringTableEntry();
            return new FText(history);
        }
        public static FText AsCultureInvariant(FString value)
        {
            return new FText();
        }

        private TextFlag _flags;
        private FTextHistory _history;
    }
}