using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;

namespace UETools.Pak
{
    public sealed partial class PakMemoryPool
    {
        private class PakMemoryArrayOwner : IMemoryOwner<byte>
        {
            private readonly static ArrayPool<byte> _arrayPool = ArrayPool<byte>.Create(_bufferSize, 50);

            public Memory<byte> Memory { get; }

            public PakMemoryArrayOwner(int size)
            {
                _array = _arrayPool.Rent(size);
                Memory = new Memory<byte>(_array, 0, size);
            }


            public void Dispose() => _arrayPool.Return(_array);

            private readonly byte[] _array;
        }
    }
}
