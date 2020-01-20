using System.Globalization;
using UnrealTools.Core;

namespace UnrealTools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal class FormatNumber : FTextHistory
        {
            protected FFormatArgumentValue SourceValue => _sourceValue;
            protected CultureInfo Culture { get; private set; } = null!;

            public override void Deserialize(FArchive reader)
            {
                base.Deserialize(reader);

                reader.Read(out _sourceValue);
                reader.Read(out bool HasFormatOptions);

                if (HasFormatOptions)
                    reader.Read(out _formatting);

                reader.Read(out _cultureName);
                Culture = CultureInfo.GetCultureInfoByIetfLanguageTag(_cultureName);
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