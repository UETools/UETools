using System;
using System.Collections.Generic;
using System.Text;

namespace UnrealTools.Pak.Interfaces
{
    interface IAesProvider
    {
        public string DecryptionKey { get; }

        public Memory<byte> Decrypt();
        public Memory<byte> Encrypt();
    }
}