using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using UnrealTools.Core;

namespace UnrealTools.Pak
{
    public sealed class NotPakFileException : UnrealException
    {
        public NotPakFileException() { }

        public NotPakFileException(string message) : base(message) { }

        public NotPakFileException(string message, Exception innerException) : base(message, innerException) { }

        private NotPakFileException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
