using UETools.Core.Enums;
using UETools.Core.Interfaces;

namespace UETools.Core.HistoryTypes
{
    class FNumberFormattingOptions : IUnrealSerializable
    {
        public bool AlwaysSign { get => _alwaysSign; set => _alwaysSign = value; }
        public bool UseGrouping { get => _useGrouping; set => _useGrouping = value; }
        internal RoundingMode RoundingMode { get => _roundingMode; set => _roundingMode = value; }
        public int MinimumIntegralDigits { get => _minimumIntegralDigits; set => _minimumIntegralDigits = value; }
        public int MaximumIntegralDigits { get => _maximumIntegralDigits; set => _maximumIntegralDigits = value; }
        public int MinimumFractionalDigits { get => _minimumFractionalDigits; set => _minimumFractionalDigits = value; }
        public int MaximumFractionalDigits { get => _maximumFractionalDigits; set => _maximumFractionalDigits = value; }

        public FArchive Serialize(FArchive reader) =>
            // TODO: Dependent on custom versions
            //reader.Read(ref _alwaysSign);
            reader.Read(ref _useGrouping)
                  .ReadUnsafe(ref _roundingMode)
                  .Read(ref _minimumIntegralDigits)
                  .Read(ref _maximumIntegralDigits)
                  .Read(ref _minimumFractionalDigits)
                  .Read(ref _maximumFractionalDigits);

        private bool _alwaysSign;
        private bool _useGrouping;
        private RoundingMode _roundingMode;
        private int _minimumIntegralDigits;
        private int _maximumIntegralDigits;
        private int _minimumFractionalDigits;
        private int _maximumFractionalDigits;

    }
}