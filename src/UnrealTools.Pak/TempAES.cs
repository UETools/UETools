using System;
using System.Collections.Generic;
using System.Text;
using UnrealTools.Pak.Interfaces;

namespace UnrealTools.Pak
{
    class TempAES : IAesProvider
    {
        public string DecryptionKey { get; }

        public Memory<byte> Decrypt()
        {
            throw new NotImplementedException();
        }

        public Memory<byte> Encrypt()
        {
            throw new NotImplementedException();
        }
    }
}
