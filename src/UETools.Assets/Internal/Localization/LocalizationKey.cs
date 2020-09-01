using System;
using System.Diagnostics.CodeAnalysis;
using UETools.Assets.Enums;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Localization
{
    internal class LocalizationKey : IUnrealSerializable, IEquatable<string>, IEquatable<LocalizationKey>
    {
        public static implicit operator LocalizationKey(string value) => new LocalizationKey(value);

        public LocalizationKey() { }
        public LocalizationKey(FString value) => _value = value;

        public FArchive Serialize(FArchive archive)
        {
            if (archive.AssetVersion >= (int)ELocResVersion.Optimized)
                archive.Read(ref _hash);

            return archive.Read(ref _value);
        }

        public override string ToString() => _value.ToString();
        public override int GetHashCode() => _value.GetHashCode();

        public bool Equals([AllowNull] string other)
        {
            if (other is null) return false;
            if (_value is null) NotDeserializedException.Throw();

            return _value.ToString() == other;
        }

        public bool Equals([AllowNull] LocalizationKey other)
        {
            if (other is null) return false;
            if (_value is null) NotDeserializedException.Throw();

            return _value == other._value;
        }

        private int _hash;
        private FString _value = null!;
    }
}
