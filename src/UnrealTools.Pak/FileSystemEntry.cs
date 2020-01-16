using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using UnrealTools.Core;
using UnrealTools.Pak.Interfaces;

namespace UnrealTools.Pak
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
        public async ValueTask<FArchive> ReadAsync(CancellationToken cancellationToken = default) => new FArchive(await ReadBufAsync(cancellationToken));


        private Memory<byte> ReadBuf()
        {
            Memory<byte> data = new byte[_fileStream.Length];
            _fileStream.ReadWholeBuf(data.Span);
            return data;
        }
        private async ValueTask<Memory<byte>> ReadBufAsync(CancellationToken cancellationToken = default)
        {
            Memory<byte> data = new byte[_fileStream.Length];
            var task = _fileStream.ReadWholeBufAsync(data, cancellationToken);
            if (!task.IsCompletedSuccessfully)
                await task;
            return data;
        }

        public void Dispose() => _fileStream.Dispose();
        public ValueTask DisposeAsync() => _fileStream.DisposeAsync();


        private FileStream _fileStream;
    }
}
