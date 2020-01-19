using System;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Core
{
    /// <summary>
    /// Unreal Engine 4 serialization of SHA1 hash
    /// </summary>
    public struct SHA1Hash : IUnrealDeserializable
    {
        /// <summary>
        /// Deserializes hash data from the stream.
        /// </summary>
        /// <param name="reader">Stream of binary data to read from.</param>
        public void Deserialize(FArchive reader) => reader.Read(out _bytes, 20);

        /// <summary>
        /// Get <see cref="System.Security.Cryptography.SHA1"/> as a bytestring.
        /// </summary>
        /// <returns>SHA1 in its <see cref="string"/> representation.</returns>
        public override string ToString() => HexString.FromByteArray(_bytes);

        private Memory<byte> _bytes;
    }
}
