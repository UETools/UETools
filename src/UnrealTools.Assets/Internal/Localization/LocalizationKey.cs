using System;
using System.Diagnostics.CodeAnalysis;
using UnrealTools.Assets.Enums;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Assets.Internal.Localization
{
    internal class LocalizationKey : IUnrealDeserializable, IEquatable<string>, IEquatable<LocalizationKey>
    {
        public static implicit operator LocalizationKey(string value) => new LocalizationKey(value);

        public LocalizationKey() { }
        public LocalizationKey(FString value) => _value = value;

        public void Deserialize(FArchive reader)
        {
            if (reader.AssetVersion >= (int)ELocResVersion.Optimized)
                reader.Read(out _hash);

            reader.Read(out _value);
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
