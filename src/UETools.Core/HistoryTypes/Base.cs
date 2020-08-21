using UETools.Core.Interfaces;

namespace UETools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class Base : FTextHistory
        {
            public FString? Value { get => _value; set => _value = value; }
            public FString? Key { get => _key; set => _key = value; }
            public FString? Namespace { get => _namespace; set => _namespace = value; }

            public Base() { }
            public Base(FString value) : this(value, null!, null!) { }
            public Base(FString value, FString localizationNamespace, FString localizationKey) => (_namespace, _key, _value) = (localizationNamespace, localizationKey, value);

            public override FArchive Serialize(FArchive reader)
            {
                reader.Read(ref _namespace)
                      .Read(ref _key)
                      .Read(ref _value);

                _localizedString = reader.Localization?.Get(_namespace, _key);

                return reader;
            }

            public override string ToString()
            {
                if (_value is null)
                    NotDeserializedException.Throw();

                return _localizedString ?? _value.ToString();
            }

            private FString? _namespace;
            private FString? _key;
            private FString? _value;
            private string? _localizedString;
        }
    }
}
