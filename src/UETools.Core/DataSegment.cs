using System;
using System.Buffers;
using System.Collections.Generic;

namespace UETools.Core
{
    public sealed class DataSegment : IDisposable
    {
        public DataSegment(IMemoryOwner<byte> data) : this(data, 0) { }
        private DataSegment(IMemoryOwner<byte> data, long runningIndex)
        {
            MemoryOwner = data;
            RunningIndex = runningIndex;
        }
        internal DataSegment(Memory<byte> data) : this(new NotOwnedOwner(data)) { }

        public IMemoryOwner<byte> MemoryOwner { get; }
        public long RunningIndex { get; private set; }
        public long Length => RunningIndex + MemoryOwner.Memory.Length;
        internal DataSegment? NextElement { get; private set; }

        public DataSegment Append(IMemoryOwner<byte> next) => NextElement is not null
                ? throw new InvalidOperationException("Can't append to segment already containing next element.")
                : (NextElement = new DataSegment(next, Length));
        public DataSegment Append(IEnumerable<IMemoryOwner<byte>> elements)
        {
            var segment = this;
            var enumerator = elements.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var it = enumerator.Current;
                segment = segment.Append(it);
            }
            return segment;
        }

        public void Dispose()
        {
            MemoryOwner.Dispose();
            NextElement?.Dispose();
        }

        public byte[] ToArray() => ToArray(0, GetLastSegment().Length);
        public byte[] ToArray(long start) => ToArray(start, GetLastSegment().Length - start);
        public byte[] ToArray(long start, long length)
        {
            var dstOffset = 0;
            var data = new byte[length];
            var element = this;
            while (element is not null)
            {
                if (element.RunningIndex < start)
                {
                    if (element.Length > start)
                    {
                        dstOffset += element.CopyTo(data, dstOffset, (int)(start - element.RunningIndex));
                    }
                }
                else
                {
                    dstOffset += element.CopyTo(data, dstOffset);
                }
                element = element.NextElement;
            }
            return data;
        }

        private int CopyTo(byte[] data, int dstOffset) => CopyTo(data, dstOffset, 0);
        private int CopyTo(byte[] data, int dstOffset, int start)
        {
            var mem = MemoryOwner.Memory.Slice(start);
            mem.Span.CopyTo(new Span<byte>(data, dstOffset, mem.Length));
            return mem.Length;
        }
        internal DataSegment GetLastSegment() => NextElement?.GetLastSegment() ?? this;

        public void Tag(string tag) => Tags.Add(tag);
        public bool HasTag(string tag) => Tags.Contains(tag);

        internal List<string> Tags { get; } = new List<string>();

        private class NotOwnedOwner : IMemoryOwner<byte>
        {
            public NotOwnedOwner(Memory<byte> data) => Memory = data;

            public Memory<byte> Memory { get; }

            public void Dispose() { }
        }
    }
}
