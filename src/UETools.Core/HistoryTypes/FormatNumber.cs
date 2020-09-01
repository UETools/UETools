using System.Globalization;
using UETools.Core;

namespace UETools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal class FormatNumber : FTextHistory
        {
            protected FFormatArgumentValue SourceValue => _sourceValue;
            protected CultureInfo Culture { get; private set; } = null!;

            public override FArchive Serialize(FArchive archive)
            {
                var hasFormatOptions = false;
                archive.Read(ref _sourceValue)
                      .Read(ref hasFormatOptions);

                if (hasFormatOptions)
                    archive.Read(ref _formatting);

                archive.Read(ref _cultureName);
                Culture = CultureInfo.GetCultureInfoByIetfLanguageTag(_cultureName);
                return archive;
            }

            protected string BuildDisplayString()
            {
                // TODO: Formatting
                if (_sourceValue is null)
                    NotDeserializedException.Throw();

                switch (SourceValue.Value)
                {
                    case long value:
                        break;
                    case ulong value:
                        break;
                    case float value:
                        break;
                    case double value:
                        break;
                }
                return base.ToString();
            }

            public override string ToString() => BuildDisplayString();


            private FString _cultureName = null!;
            private FFormatArgumentValue _sourceValue = null!;
            private FNumberFormattingOptions? _formatting;
        }
    }
}