using UnrealTools.Core.Interfaces;

namespace UnrealTools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class Base : FTextHistory
        {
            public Base() : this(null!) { }
            public Base(FString value) : this(value, null!, null!) { }
            public Base(FString value, FString localizationNamespace, FString localizationKey) => (_locNamespace, _locKey, _value) = (localizationNamespace, localizationKey, value);

            public override void Deserialize(FArchive reader)
            {
                base.Deserialize(reader);

                reader.Read(out _locNamespace);
                reader.Read(out _locKey);
                reader.Read(out _value); 
                _localizedString = reader.Localization?.Get(_locNamespace, _locKey);
            }

            public override string ToString()
            {
                if (_locNamespace is null || _locKey is null)
                {
                    if (_value is null)
                        NotDeserializedException.Throw();

                    return _value.ToString();
                }

                return _localizedString ?? _value.ToString();
            }

            private FString _locNamespace = null!;
            private FString _locKey = null!;
            private FString _value = null!;
            private string? _localizedString;
        }
    }
}
