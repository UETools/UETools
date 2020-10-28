using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class MemoryReader : IUnrealValueReader
    {
        private Memory<byte> CurrentBuffer => CurrentSegment.MemoryOwner.Memory;
        private int CurrentBufferPosition => (int)(Position - CurrentSegment.RunningIndex);

        public long Length => _lastSegment.Length;
        public long Position
        {
            get => _position;
            set
            {
                _position = value;
                UpdateBuffer();
            }
        }

        private DataSegment CurrentSegment { get; set; }

        private void UpdateBuffer()
        {
            if (CurrentSegment.Length > _position)
                return;
            else if (CurrentSegment.NextElement is not null)
            {
                CurrentSegment = CurrentSegment.NextElement;
                UpdateBuffer();
            }
        }

        public MemoryReader(IMemoryOwner<byte> owner) : this(new DataSegment(owner)) { }
        public MemoryReader(Memory<byte> memory) : this(new DataSegment(memory)) { }
        public MemoryReader(DataSegment firstSegment)
        {
            CurrentSegment = _firstSegment = firstSegment;
            _lastSegment = _firstSegment.GetLastSegment();
        }

        private MemoryReader(DataSegment firstSegment, long startOffset) : this(firstSegment) => Position = startOffset;
        private MemoryReader(DataSegment firstSegment, long startOffset, long length) : this(firstSegment, startOffset)
        {
            var segment = CurrentSegment;
            while (segment is not null)
            {
                if(segment.Length < length)
                    segment = segment.NextElement;
                else
                {
                    // TODO: Slice of original segment instead of whole
                    _lastSegment = segment;
                    break;
                }
            }
        }

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

        public Memory<byte> ReadBytes(int count)
        {
            var buf = CurrentBuffer;
            var pos = CurrentBufferPosition;
            if (buf.Length < pos + count)
                ThrowOutOfRange(nameof(count), Length - pos, "Requested amount of bytes exceeds the current segment size.");

            var data = buf.Slice(pos, count);
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

        public IUnrealValueReader Slice(long start)
        {
            return new MemoryReader(_firstSegment, start);
        }
        public IUnrealValueReader Slice(long start, long length)
        {
            return new MemoryReader(_firstSegment, start, length);
        }


        private string BytesToString(Encoding encoding, int length, ReadOnlyMemory<byte> memory)
        {
#if NETSTANDARD2_0
            var result = new string((char)0, length);
            using var x = memory.Pin();
            unsafe
            {
                fixed (char* chars = result)
                {
                    encoding.GetChars((byte*)x.Pointer, memory.Length, chars, length);
                }
            }
            return result;
#else
            return string.Create(length, (Memory: memory, Encoding: encoding), (buffer, state) 
                => state.Encoding.GetChars(state.Memory.Span, buffer));
#endif
        }
        public string ReadUnrealString(int length)
        {
            var pos = CurrentBufferPosition;
            if (length > 0)
            {
                Position += length;
                return BytesToString(Encoding.UTF8, length - 1, CurrentBuffer.Slice(pos, length - 1));
            }
            else if (length < 0)
            {
                var len = -length * 2;
                Position += len;
                return BytesToString(Encoding.Unicode, -length - 1, CurrentBuffer.Slice(pos, len - 2));
            }
            else
                return string.Empty;
        }

        private T ReadValue<T>() where T : struct => MemoryMarshal.Read<T>(ReadSpanBytes(Unsafe.SizeOf<T>()));

        public void Dispose() => _firstSegment?.Dispose();

        [DoesNotReturn]
        private static void ThrowOutOfRange(string argument, object actualValue, string message) => throw new ArgumentOutOfRangeException(argument, actualValue, message);

        private readonly DataSegment _firstSegment;
        private readonly DataSegment _lastSegment;
        private long _position;
    }
}
