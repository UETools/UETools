using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace UETools.Pak
{
    public sealed partial class PakMemoryPool : MemoryPool<byte>
    {
        private PakMemoryPool() { }
        private const int _bufferSize = 1024 * 1024 * 256;
        public override int MaxBufferSize => _bufferSize;
        public override IMemoryOwner<byte> Rent(int minBufferSize = -1)
        {
            if (minBufferSize == -1)
                minBufferSize = 65536;

            return new PakMemoryArrayOwner(minBufferSize);
        }
        protected override void Dispose(bool disposing)
        {
        }

        [DoesNotReturn]
        private void ThrowOutOfRange(string paramName) => throw new ArgumentOutOfRangeException(paramName);
        public static new PakMemoryPool Shared { get; } = new PakMemoryPool();
    }
}
