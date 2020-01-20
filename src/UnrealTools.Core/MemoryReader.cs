using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class MemoryReader : IUnrealValueReader
    {
        private Memory<byte> Buffer => _owner is null ? _buffer : _owner.Memory;
        private int IntPosition => (int)Position;

        public long Length => Buffer.Length;
        public long Position { get; set; }

        public MemoryReader(IMemoryOwner<byte> owner) => _owner = owner;
        public MemoryReader(Memory<byte> memory) => _buffer = memory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public long Seek(long offset, SeekOrigin origin) => origin switch
        {
            SeekOrigin.Begin => Position = offset,
            SeekOrigin.Current => Position += offset,
            SeekOrigin.End => Position = Length - offset,
            _ => throw new NotImplementedException()
        };

        public Memory<byte> GetBuffer() => Buffer;
        public Span<byte> GetSpanBuffer() => Buffer.Span;

        public Memory<byte> ReadBytes(int count)
        {
            if (Buffer.Length < IntPosition + count)
                ThrowEndOfStream();

            var data = Buffer.Slice(IntPosition, count);
            Position += count;
            return data;
        }

        public Span<byte> ReadSpanBytes(int count) => ReadBytes(count).Span;

        public bool ReadBoolean() => ReadInt32() != 0;
        public sbyte ReadSByte() => ReadValue<sbyte>();
        public short ReadInt16() => ReadValue<short>();
        public int ReadInt32() => ReadValue<int>();
        public long ReadInt64() => ReadValue<long>();
        public byte ReadByte() => ReadValue<byte>();
        public ushort ReadUInt16() => ReadValue<ushort>();
        public uint ReadUInt32() => ReadValue<uint>();
        public ulong ReadUInt64() => ReadValue<ulong>();
        public float ReadSingle() => ReadValue<float>();
        public double ReadDouble() => ReadValue<double>();
        public decimal ReadDecimal() => ReadValue<decimal>();
        public string ReadByteString() => ReadUnrealString(ReadByte());

        public string ReadUnrealString(int length)
        {
            var pos = IntPosition;
            if (length > 0)
            {
                Position += length;
                return string.Create(length - 1, Buffer.Slice(pos, length - 1), (Span<char> buf, Memory<byte> mem) =>
                    Encoding.UTF8.GetChars(mem.Span, buf));
            }
            else if (length < 0)
            {
                var len = -length * 2;
                Position += len;
                return string.Create(-length - 1, Buffer.Slice(pos, len - 2), (Span<char> buf, Memory<byte> mem) =>
                    Encoding.Unicode.GetChars(mem.Span, buf));
            }
            else
                return string.Empty;
        }

        private T ReadValue<T>() where T : struct => MemoryMarshal.Read<T>(ReadSpanBytes(Unsafe.SizeOf<T>()));

        public void Dispose() => _owner?.Dispose();

        [DoesNotReturn]
        private static void ThrowEndOfStream() => throw new EndOfStreamException();

        private readonly Memory<byte> _buffer;
        private readonly IMemoryOwner<byte>? _owner;
    }
}
