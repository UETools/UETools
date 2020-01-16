using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnrealTools.Pak
{
    public sealed partial class PakInfo
    {
        private ref struct SpanReader
        {
            private Span<byte> _data;

            public int Remaining => _data.Length;

            internal SpanReader(Span<byte> data) => _data = data;

            private T ReadValue<T>() where T : unmanaged
            {
                var size = Unsafe.SizeOf<T>();
                var sp = _data;
                _data = _data.Slice(size);
                return MemoryMarshal.Read<T>(sp);

            }
            private Span<T> ReadValues<T>(int len) where T : unmanaged
            {
                var size = Unsafe.SizeOf<T>() * len;
                var sp = _data.Slice(0, size);
                _data = _data.Slice(size);
                return MemoryMarshal.Cast<byte, T>(sp);
            }
            private Span<byte> ReadValues(int len)
            {
                var sp = _data.Slice(0, len);
                _data = _data.Slice(len);
                return sp;
            }
            public void Read<T>(out T item) where T : unmanaged => item = ReadValue<T>();
            public void Read<T>(out Span<T> item, int len) where T : unmanaged => item = ReadValues<T>(len);
            public void Read<T>(out Memory<T> item, int len) where T : unmanaged => item = ReadValues<T>(len).ToArray();
            public void Read(out Span<byte> item, int len) => item = ReadValues(len);
            public void Read(out Memory<byte> item, int len) => item = ReadValues(len).ToArray();
        }
    }
}