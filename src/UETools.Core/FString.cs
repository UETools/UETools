using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    /// <summary>
    /// Unreal Engine 4 FString implementation.
    /// </summary>
    /// <remarks><see langword="string"/> class implementation doesn't contain parameterless constructor, which made this class necessary.</remarks>
    [DebuggerDisplay("{Value}")]
    public sealed partial class FString : IUnrealDeserializable, IEnumerable<char>, IEquatable<FString>, IEquatable<string>, IComparable<string>, IComparable
    {
        /// <summary>
        /// Deserialized value of the string instance.
        /// </summary>
        public string Value { get => _value; private set => _value = value; }
        /// <summary>
        /// Length of the string, as serialized.
        /// </summary>
        /// <remarks>Negative length denotes UTF16</remarks>
        public int Length => IsWide ? -_length : _length;
        /// <summary>
        /// Returns whether strings is using <see cref="UTF8Encoding"/> or <see cref="UnicodeEncoding"/>
        /// </summary>
        /// <value><see langword="true"/> if <see langword="string"/> is encoded using <see cref="UnicodeEncoding"/> or <see langword="false"/> if <see cref="UTF8Encoding"/></value>
        /// <remarks>Negative length denotes UTF16</remarks>
        public bool IsWide => _length < 0;

        /// <summary>
        /// Creates new string with <see cref="string.Empty"/> as value.
        /// </summary>
        public FString() : this(string.Empty) { }
        /// <summary>
        /// Creates new string using <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Value to assign to the underlying string.</param>
        public FString(string value) => _value = value;

        /// <inheritdoc />
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _length);
            reader.Read(out _value, _length);
        }

        private int ByteCount => Length * CharSize;
        private int CharSize
        {
            get
            {
                if (!_charSize.HasValue)
                    _charSize = UsedEncoding.GetByteCount(" ");

                return _charSize.Value;
            }
        }
        private Encoding UsedEncoding => IsWide ? Encoding.Unicode : Encoding.UTF8;

        private int _length;
        private int? _charSize;
        private string _value;
    }
}
