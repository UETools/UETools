using System;
using System.Diagnostics.CodeAnalysis;

namespace UnrealTools.Core
{
    public class NotDeserializedException : UnrealException
    {
        [DoesNotReturn]
        public static void Throw() => throw new NotDeserializedException();

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
