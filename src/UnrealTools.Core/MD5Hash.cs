using System;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Core
{
    /// <summary>
    /// Unreal Engine 4 serialization of MD5 hash
    /// </summary>
    public struct MD5Hash : IUnrealDeserializable
    {
        /// <summary>
        /// Deserializes hash data from the stream.
        /// </summary>
        /// <param name="reader">Stream of binary data to read from.</param>
        public void Deserialize(FArchive reader)
        {
            reader.Read(out bool isValid);
            if (isValid)
                reader.Read(out _bytes, 16);
        }
        /// <summary>
        /// Get <see cref="System.Security.Cryptography.MD5"/> as a bytestring.
        /// </summary>
        /// <returns>MD5 in its <see cref="string"/> representation, or empty string if the hash is invalid.</returns>
        public override string ToString() => HexString.FromByteArray(_bytes);

        private Memory<byte> _bytes;
    }
}
