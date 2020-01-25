using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using UnrealTools.Core;

namespace UnrealTools.Pak
{
    class PakEncryptedException : UnrealException
    {
        public PakEncryptedException()
        {
        }

        public PakEncryptedException(string message) : base(message)
        {
        }

        public PakEncryptedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PakEncryptedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
