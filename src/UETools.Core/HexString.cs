using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace UETools.Core
{
    public static class HexString
    {
        static readonly uint[] _lookup32 = Enumerable.Range(0, 256).Select(i =>
        {
            string s = i.ToString("X2");
            if (BitConverter.IsLittleEndian)
                return s[0] + ((uint)s[1] << 16);
            else
                return s[1] + ((uint)s[0] << 16);
        }).ToArray();

        public static string FromByteArray(ReadOnlyMemory<byte> bytes) => string.Create(bytes.Length * 2, bytes, (chars, b) =>
                                                                                    {
                                                                                        var span = bytes.Span;
                                                                                        var x = MemoryMarshal.Cast<char, uint>(chars);
                                                                                        for (var i = bytes.Length - 1; i >= 0; i--)
                                                                                        {
                                                                                            x[i] = _lookup32[span[i]];
                                                                                        }
                                                                                    });
    }
}
