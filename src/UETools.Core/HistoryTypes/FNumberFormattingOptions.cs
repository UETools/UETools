using UETools.Core.Enums;
using UETools.Core.Interfaces;

namespace UETools.Core.HistoryTypes
{
    class FNumberFormattingOptions : IUnrealDeserializable
    {
        public bool AlwaysSign { get => _alwaysSign; set => _alwaysSign = value; }
        public bool UseGrouping { get => _useGrouping; set => _useGrouping = value; }
        internal RoundingMode RoundingMode { get => _roundingMode; set => _roundingMode = value; }
        public int MinimumIntegralDigits { get => _minimumIntegralDigits; set => _minimumIntegralDigits = value; }
        public int MaximumIntegralDigits { get => _maximumIntegralDigits; set => _maximumIntegralDigits = value; }
        public int MinimumFractionalDigits { get => _minimumFractionalDigits; set => _minimumFractionalDigits = value; }
        public int MaximumFractionalDigits { get => _maximumFractionalDigits; set => _maximumFractionalDigits = value; }

        public void Deserialize(FArchive reader)
        {
            // TODO: Dependent on custom versions
            //reader.Read(out _alwaysSign);
            reader.Read(out _useGrouping);
            reader.ReadUnsafe(out _roundingMode);
            reader.Read(out _minimumIntegralDigits);
            reader.Read(out _maximumIntegralDigits);
            reader.Read(out _minimumFractionalDigits);
            reader.Read(out _maximumFractionalDigits);
        }

        private bool _alwaysSign;
        private bool _useGrouping;
        private RoundingMode _roundingMode;
        private int _minimumIntegralDigits;
        private int _maximumIntegralDigits;
        private int _minimumFractionalDigits;
        private int _maximumFractionalDigits;

    }
}