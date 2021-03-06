﻿using System;
using System.Buffers;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using UETools.Core;
using UETools.Pak.Interfaces;

namespace UETools.Pak
{
    public sealed class FileSystemEntry : IAsyncDisposable, IDisposable, IEntry
    {
        public FileSystemEntry(string path)
            : this(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, PakFile.BufferSize))
        {

        }
        private FileSystemEntry(FileStream fileStream)
            => _fileStream = fileStream;

        public FArchive Read() => new FArchive(ReadBuf());
        public async ValueTask<FArchive> ReadAsync(CancellationToken cancellationToken = default) => new FArchive(await ReadBufAsync(cancellationToken).ConfigureAwait(false));

        private IMemoryOwner<byte> ReadBuf()
        {
            var data = PakMemoryPool.Shared.Rent((int)_fileStream.Length);
            _fileStream.ReadWholeBuf(data.Memory.Span);
            return data;
        }
        private async ValueTask<IMemoryOwner<byte>> ReadBufAsync(CancellationToken cancellationToken = default)
        {
            var data = PakMemoryPool.Shared.Rent((int)_fileStream.Length);
            await _fileStream.ReadWholeBufAsync(data.Memory, cancellationToken).ConfigureAwait(false);
            return data;
        }

        public void Dispose() => _fileStream.Dispose();
        public ValueTask DisposeAsync()
        {
#if NETSTANDARD2_0
            _fileStream.Dispose();
            return default;
#else
            return _fileStream.DisposeAsync();
#endif
        }


        private FileStream _fileStream;
    }
}
