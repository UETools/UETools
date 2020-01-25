using System;
using System.Collections.Generic;
using System.Text;

namespace UnrealTools.Pak.Interfaces
{
    /// <summary>
    /// Interface for providing AES encryption/decryption key.
    /// </summary>
    public interface IAesKeyProvider
    {
        /// <summary>
        /// Base64 encoded encryption key.
        /// </summary>
        public string DecryptionKey { get; }
    }
}