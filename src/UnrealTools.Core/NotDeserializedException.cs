using System;

namespace UnrealTools.Core
{
    public class NotDeserializedException : UnrealException
    {
        public NotDeserializedException()
        {
        }

        public NotDeserializedException(string message) : base(message)
        {
        }

        public NotDeserializedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
