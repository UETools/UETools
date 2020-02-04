using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace UnrealTools.Pak
{
    internal class ZlibStream : Stream
    {
        private static readonly ReadOnlyMemory<byte> _zlibHeader = new byte[] { 0x78, 0x9C };

        public ZlibStream(Stream stream, CompressionMode mode)
        {
            var header = _zlibHeader.Span;
            if (mode != CompressionMode.Decompress)
                stream.Write(header);
            else
            {
                if (stream.ReadByte() != header[0] || stream.ReadByte() != header[1])
                    throw new InvalidDataException("Not a zlib stream.");
            }
            _deflateStream = new DeflateStream(stream, mode);
        }
        public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen)
        {
            var header = _zlibHeader.Span;
            if (mode != CompressionMode.Decompress)
                stream.Write(header);
            else
            {
                Span<byte> bytes = stackalloc byte[2];
                stream.Read(bytes);
                if (!bytes.SequenceEqual(header))
                    throw new InvalidDataException("Not a zlib stream.");
            }

            _deflateStream = new DeflateStream(stream, mode, leaveOpen);
        }

        /// <inheritdoc cref="DeflateStream.CanRead"/>
        public override bool CanRead => _deflateStream.CanRead;
        /// <inheritdoc cref="DeflateStream.CanSeek"/>
        public override bool CanSeek => _deflateStream.CanSeek;
        /// <inheritdoc cref="DeflateStream.CanWrite"/>
        public override bool CanWrite => _deflateStream.CanWrite;
        /// <inheritdoc cref="DeflateStream.Length"/>
        public override long Length => _deflateStream.Length;
        /// <inheritdoc cref="DeflateStream.Position"/>
        public override long Position { get => _deflateStream.Position; set => _deflateStream.Position = value; }

        /// <inheritdoc cref="DeflateStream.Flush"/>
        public override void Flush() => _deflateStream.Flush();
        /// <inheritdoc cref="DeflateStream.Read(byte[], int, int)"/>
        public override int Read(byte[] buffer, int offset, int count) => _deflateStream.Read(buffer, offset, count);
        /// <inheritdoc cref="DeflateStream.Seek"/>
        public override long Seek(long offset, SeekOrigin origin) => _deflateStream.Seek(offset, origin);
        /// <inheritdoc cref="DeflateStream.SetLength"/>
        public override void SetLength(long value) => _deflateStream.SetLength(value);
        /// <inheritdoc cref="DeflateStream.Write(byte[], int, int)"/>
        public override void Write(byte[] buffer, int offset, int count) => _deflateStream.Write(buffer, offset, count);
        /// <inheritdoc cref="DeflateStream.CopyTo"/>
        public override void CopyTo(Stream destination, int bufferSize) => _deflateStream.CopyTo(destination, bufferSize);
        /// <inheritdoc cref="DeflateStream.CopyToAsync"/>
        public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken) => _deflateStream.CopyToAsync(destination, bufferSize, cancellationToken);
        /// <inheritdoc cref="DeflateStream.CopyToAsync"/>
        protected override void Dispose(bool disposing) => _deflateStream.Dispose();

        /// <inheritdoc cref="DeflateStream.BeginRead"/>
        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object? state) => _deflateStream.BeginRead(buffer, offset, count, callback, state);
        /// <inheritdoc cref="DeflateStream.EndRead"/>
        public override int EndRead(IAsyncResult asyncResult) => _deflateStream.EndRead(asyncResult);
        /// <inheritdoc cref="DeflateStream.BeginWrite"/>
        public override IAsyncResult BeginWrite(byte[] array, int offset, int count, AsyncCallback asyncCallback, object? asyncState) => _deflateStream.BeginWrite(array, offset, count, asyncCallback, asyncState);
        /// <inheritdoc cref="DeflateStream.EndWrite"/>
        public override void EndWrite(IAsyncResult asyncResult) => _deflateStream.EndWrite(asyncResult);
        /// <inheritdoc cref="DeflateStream.FlushAsync"/>
        public override Task FlushAsync(CancellationToken cancellationToken) => _deflateStream.FlushAsync(cancellationToken);

        /// <inheritdoc cref="DeflateStream.Read(Span{byte})"/>
        public override int Read(Span<byte> buffer) => _deflateStream.Read(buffer);
        /// <inheritdoc cref="DeflateStream.ReadAsync(byte[], int, int, CancellationToken)"/>
        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => _deflateStream.ReadAsync(buffer, offset, count, cancellationToken);
        /// <inheritdoc cref="DeflateStream.ReadAsync(Memory{byte}, CancellationToken)"/>
        public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default) => _deflateStream.ReadAsync(buffer, cancellationToken);
        /// <inheritdoc cref="DeflateStream.ReadByte"/>
        public override int ReadByte() => _deflateStream.ReadByte();
        /// <inheritdoc cref="DeflateStream.Write(ReadOnlySpan{byte})"/>
        public override void Write(ReadOnlySpan<byte> buffer) => _deflateStream.Write(buffer);
        /// <inheritdoc cref="DeflateStream.WriteAsync(byte[], int, int, CancellationToken)"/>
        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => _deflateStream.WriteAsync(buffer, offset, count, cancellationToken);
        /// <inheritdoc cref="DeflateStream.WriteAsync(ReadOnlyMemory{byte}, CancellationToken)"/>
        public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default) => _deflateStream.WriteAsync(buffer, cancellationToken);

        private readonly DeflateStream _deflateStream;
    }
}
