using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace UnrealTools.Core
{
    public partial class FString
    {
        /// <summary>
        /// Implicit conversion from Unreal <see cref="FString"/> instance to <see langword="string"/>.
        /// </summary>
        /// <param name="value">Instance to get string from.</param>
        public static implicit operator string(FString value) => value.ToString();
        /// <summary>
        /// Implicit conversion from <see langword="string"/> to Unreal <see cref="FString"/> instance.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator FString(string value) => new FString(value);

        /// <summary>
        /// Gets underlying <see langword="string"/> value.
        /// </summary>
        /// <returns>Underlying <see langword="string"/>.</returns>
        public override string ToString()
        {
            if (Value is null)
                NotDeserializedException.Throw();

            return Value;
        }
        /// <summary>
        /// Gets combined hash of underlying <see langword="string"/> and its <see cref="Encoding"/>.
        /// </summary>
        /// <returns>Combined hash of underlying <see langword="string"/> and recognized <see cref="Encoding"/></returns>
        public override int GetHashCode() => HashCode.Combine(Value.GetHashCode(), UsedEncoding.GetHashCode()); // Strings might be the same, but with different encoding? not sure if I should differentiate that

        /// <summary>
        /// Determines whether the this instance and another specified <see langword="object"/> are equal, trying to use specialized comparer when possible.
        /// </summary>
        /// <param name="obj">The <see langword="object"/> to compare with the current <see langword="object"/>.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object? obj) =>
            obj switch
            {
                FString str => Equals(str),
                string str => Equals(str),
                _ => base.Equals(obj)
            };
        /// <summary>
        /// Determines whether this instance and another specified <see langword="string"/> object have the same value.
        /// </summary>
        /// <param name="other">The <see langword="string"/> to compare to this instance.</param>
        /// <returns><see langword="true"/> if the value of the value parameter is the same as the value of this instance; otherwise, <see langword="false"/>. If value is <see langword="null"/>, the method returns <see langword="false"/>.</returns>
        public bool Equals(string other) => Value.Equals(other);
        /// <summary>
        /// Determines whether this instance and another specified <see cref="FString"/> object have the same underlying <see langword="string"/> value.
        /// </summary>
        /// <param name="other">The instance to compare with.</param>
        /// <returns><see langword="true"/> if the value of the value parameter is the same as the value of this instance; otherwise, <see langword="false"/>. If value is <see langword="null"/>, the method returns <see langword="false"/>.</returns>
        public bool Equals(FString other) => Value.Equals(other.Value);

        /// <summary>
        /// Retrieves an object that can iterate through the individual characters in the underlying string.
        /// </summary>
        /// <returns>An enumerator object of the underlying string.</returns>
        public CharEnumerator GetEnumerator() => Value.GetEnumerator();
        IEnumerator<char> IEnumerable<char>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Compares this instance with a specified <see langword="string"/> and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see langword="string"/>.
        /// </summary>
        /// <param name="other">The <see langword="string"/> to compare with this instance.</param> 
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="other"/> parameter.</returns>
        public int CompareTo(string other) => Value.CompareTo(other);
        /// <summary>
        /// Compares this instance with a specified <see langword="object"/> and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see langword="object"/>.
        /// </summary>
        /// <param name="obj">An <see langword="object"/> that evaluates to a <see langword="string"/>.</param> 
        /// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="obj"/> parameter.</returns>
        public int CompareTo(object? obj) => Value.CompareTo(obj);

        /// <summary>
        /// Determines whether two specified <see cref="FString"/> instances have the same value.
        /// </summary>
        /// <param name="first">First instance to use for comparision.</param>
        /// <param name="second">Second instance to use for comparision.</param>
        /// <returns><see langword="true"/> if parameters are equal; otherwise <see langword="false"/>.</returns>
        public static bool operator ==(FString first, FString second) => first.Equals(second);
        /// <summary>
        /// Determines whether two specified <see cref="FString"/> instances have the different value.
        /// </summary>
        /// <param name="first">First instance to use for comparision.</param>
        /// <param name="second">Second instance to use for comparision.</param>
        /// <returns><see langword="true"/> if parameters are not equal; otherwise <see langword="false"/>.</returns>
        public static bool operator !=(FString first, FString second) => !first.Equals(second);
        /// <summary>
        /// Determines whether <see langword="string"/> and <see cref="Value"/> have the same value.
        /// </summary>
        /// <param name="first"><see langword="string"/> to use for comparision.</param>
        /// <param name="second"><see cref="FString"/> instance to use for comparision.</param>
        /// <returns><see langword="true"/> if parameters are equal; otherwise <see langword="false"/>.</returns>
        public static bool operator ==(string first, FString second) => first.Equals(second.Value);
        /// <summary>
        /// Determines whether <see langword="string"/> and <see cref="Value"/> have the different value.
        /// </summary>
        /// <param name="first"><see langword="string"/> to use for comparision.</param>
        /// <param name="second"><see cref="FString"/> instance to use for comparision.</param>
        /// <returns><see langword="true"/> if parameters are not equal; otherwise <see langword="false"/>.</returns>
        public static bool operator !=(string first, FString second) => !first.Equals(second.Value);
        /// <summary>
        /// Determines whether <see cref="Value"/> and <see langword="string"/> have the same value.
        /// </summary>
        /// <param name="first"><see cref="FString"/> instance to use for comparision.</param>
        /// <param name="second"><see langword="string"/> to use for comparision.</param>
        /// <returns><see langword="true"/> if parameters are equal; otherwise <see langword="false"/>.</returns>
        public static bool operator ==(FString first, string second) => first.Equals(second);
        /// <summary>
        /// Determines whether <see cref="Value"/> and <see langword="string"/> have the different value.
        /// </summary>
        /// <param name="first"><see cref="FString"/> instance to use for comparision.</param>
        /// <param name="second"><see langword="string"/> to use for comparision.</param>
        /// <returns><see langword="true"/> if parameters are not equal; otherwise <see langword="false"/>.</returns>
        public static bool operator !=(FString first, string second) => !first.Equals(second);
    }
}
