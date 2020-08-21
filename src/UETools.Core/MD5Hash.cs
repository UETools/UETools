using System;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    /// <summary>
    /// Unreal Engine 4 serialization of MD5 hash
    /// </summary>
    public struct MD5Hash : IUnrealSerializable
    {
        /// <summary>
        /// Deserializes hash data from the stream.
        /// </summary>
        /// <param name="reader">Stream of binary data to read from.</param>
        public FArchive Serialize(FArchive reader)
        {
            var isValid = !_bytes.IsEmpty;
            reader.Read(ref isValid);
            if (isValid)
                reader.Read(ref _bytes, 16);

            return reader;
        }
        /// <summary>
        /// Get <see cref="System.Security.Cryptography.MD5"/> as a bytestring.
        /// </summary>
        /// <returns>MD5 in its <see cref="string"/> representation, or empty string if the hash is invalid.</returns>
        public override string ToString() => HexString.FromByteArray(_bytes);

        private Memory<byte> _bytes;
    }
}
