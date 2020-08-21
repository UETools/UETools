using System;
using System.Diagnostics;
using UETools.Core.Enums;
using UETools.Core.HistoryTypes;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    [DebuggerDisplay("FText: {ToString()}")]
    public sealed class FText : IUnrealSerializable
    {
        public FText() : this(null!) { }
        private FText(FTextHistory history) => _history = history;

        public FArchive Serialize(FArchive reader)
        {
            reader.ReadUnsafe(ref _flags);
            if (reader.Version < UE4Version.VER_UE4_FTEXT_HISTORY)
            {
                var baseHistory = (FTextHistory.Base)_history;
                FString? _value = baseHistory.Value;
                reader.Read(ref _value);
                if (reader.Version >= UE4Version.VER_UE4_ADDED_NAMESPACE_AND_KEY_DATA_TO_FTEXT)
                    _history = new FTextHistory.Base(_value);
                else
                {
                    FString? _namespace = baseHistory.Namespace, _key = baseHistory.Key;
                    reader.Read(ref _namespace)
                          .Read(ref _key);
                    _history = new FTextHistory.Base(_value, _namespace, _key);
                }
            }
            else
            {
                TextHistoryType HistoryType = TextHistoryType.None;
                reader.ReadUnsafe(ref HistoryType);
                if (FTextHistory.HistoryTypes.TryGetValue(HistoryType, out var init))
                {
                    _history = init();
                    _history.Serialize(reader);
                }
                else
                    Debug.WriteLine($"HistoryType unrecognized: {HistoryType}");
            }
            return reader;
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
        public static FText FromDate(DateTime date)
        {
            var history = new FTextHistory.AsDate(date);
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