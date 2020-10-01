using System;
using System.Buffers;

namespace UETools.Core
{
    public static class StringHelper
    {
#if NETSTANDARD2_0
        public delegate void SpanAction<T, in TArg>(Span<T> span, TArg arg);
#endif
        public static string Create<T>(int length, T state, SpanAction<char, T> action)
        {
#if NETSTANDARD2_0
            unsafe
            {
                var str = new string((char)0, length);
                fixed(char* chars = str)
                {
                    var span = new Span<char>(chars, length);
                    action(span, state);
                }
                return str;
            }
#else
            return string.Create(length, state, action);
#endif
        }
    }
}
