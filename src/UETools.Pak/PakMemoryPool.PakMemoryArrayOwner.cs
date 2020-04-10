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

            public PakMemoryArrayOwner(int size)
            {
                _array = _arrayPool.Rent(size);
                _intendedSize = size;
            }

            public Memory<byte> Memory
            {
                get
                {
                    if (_array is null) ThrowDisposed();

                    return new Memory<byte>(_array, 0, _intendedSize);
                }
            }
            public void Dispose()
            {
                if (_array is null)
                    return;
                else
                    _arrayPool.Return(_array);
            }

            [DoesNotReturn]
            private void ThrowDisposed() => throw new ObjectDisposedException(nameof(PakMemoryArrayOwner));

            private readonly byte[] _array;
            private readonly int _intendedSize;
        }
    }
}
