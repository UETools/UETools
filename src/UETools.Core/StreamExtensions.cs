using System;
using System.Buffers;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace UETools.Core
{
    public static partial class StreamExtensions
    {
        public static void ReadWholeBuf(this Stream stream, Span<byte> buf)
        {
            var remaining = buf.Length;
            var offset = 0;
            while (remaining > 0)
            {
                var read = stream.Read(buf.Slice(offset));
                if (read <= 0)
                    throw new EndOfStreamException($"End of stream reached, {remaining} bytes left to read");

                remaining -= read;
                offset += read;
            }
        }
        public static void ReadWholeBuf(this Stream stream, byte[] buf) => stream.ReadCount(buf, buf.Length);
        public static void ReadWholeBuf(this Stream stream, long offset, in Span<byte> buf) => stream.ReadWholeBuf(offset, SeekOrigin.Begin, buf);
        public static void ReadWholeBuf(this Stream stream, long offset, SeekOrigin origin, in Span<byte> buf)
        {
            stream.Seek(offset, origin);
            stream.ReadWholeBuf(buf);
        }
        public static void ReadCount(this Stream stream, byte[] buf, int count) => stream.ReadCount(buf, 0, buf.Length, count);
        public static void ReadCount(this Stream stream, byte[] buf, int bufOffset, int bufCount, int count)
        {
            if (count > bufCount)
                throw new ArgumentOutOfRangeException(nameof(count));

            var remaining = count;
            var offset = bufOffset;
            while (remaining > 0)
            {
                var read = stream.Read(buf, offset, remaining);
                if (read <= 0)
                    throw new EndOfStreamException($"End of stream reached, {remaining} bytes left to read");

                remaining -= read;
                offset += read;
            }
        }

        public static async ValueTask ReadWholeBufAsync(this Stream stream, Memory<byte> buf, CancellationToken cancellationToken = default)
        {
            var remaining = buf.Length;
            var offset = 0;
            while (remaining > 0)
            {
                var read = await stream.ReadAsync(buf.Slice(offset), cancellationToken).ConfigureAwait(false);
                if (read <= 0)
                    throw new EndOfStreamException($"End of stream reached, {remaining} bytes left to read");

                remaining -= read;
                offset += read;

            }
        }
        public static ValueTask ReadWholeBufAsync(this Stream stream, long offset, Memory<byte> buf, CancellationToken cancellationToken = default) => stream.ReadWholeBufAsync(offset, SeekOrigin.Begin, buf, cancellationToken);
        public static ValueTask ReadWholeBufAsync(this Stream stream, long offset, SeekOrigin origin, Memory<byte> buf, CancellationToken cancellationToken = default)
        {
            stream.Seek(offset, SeekOrigin.Begin);
            return stream.ReadWholeBufAsync(buf, cancellationToken);
        }
        public static ValueTask ReadCountAsync(this Stream stream, byte[] buf, int count, CancellationToken cancellationToken = default) => stream.ReadCountAsync(buf, 0, buf.Length, count, cancellationToken);
        public static async ValueTask ReadCountAsync(this Stream stream, byte[] buf, int bufOffset, int bufCount, int count, CancellationToken cancellationToken = default)
        {
            if (count > bufCount || count > bufCount - bufOffset)
                throw new ArgumentOutOfRangeException(nameof(count));

            var remaining = count;
            var offset = bufOffset;
            while (remaining > 0)
            {
                var read = await stream.ReadAsync(buf, offset, remaining, cancellationToken).ConfigureAwait(false);
                if (read <= 0)
                    throw new EndOfStreamException($"End of stream reached, {remaining} bytes left to read");

                remaining -= read;
                offset += read;
            }
        }

        public static int Read(this Stream stream, Memory<byte> buffer)
        {
            if (MemoryMarshal.TryGetArray<byte>(buffer, out var array))
            {
                return stream.Read(array.Array!, array.Offset, array.Count);
            }
            else
            {
                var sharedBuffer = ArrayPool<byte>.Shared.Rent(buffer.Length);
                try
                {
                    var result = stream.Read(sharedBuffer, 0, buffer.Length);
                    new Span<byte>(sharedBuffer, 0, result).CopyTo(buffer.Span);
                    return result;
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(sharedBuffer);
                }
            }
        }
#if NETSTANDARD2_0
        public static int Read(this Stream stream, Span<byte> buffer)
        {
            var sharedBuffer = ArrayPool<byte>.Shared.Rent(buffer.Length);
            try
            {
                var result = stream.Read(sharedBuffer, 0, buffer.Length);
                new Span<byte>(sharedBuffer, 0, result).CopyTo(buffer);
                return result;
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(sharedBuffer);
            }
        }
        public static void Write(this Stream stream, ReadOnlySpan<byte> buffer)
        {
            var sharedBuffer = ArrayPool<byte>.Shared.Rent(buffer.Length);
            try
            {
                buffer.CopyTo(sharedBuffer);
                stream.Write(sharedBuffer, 0, buffer.Length);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(sharedBuffer);
            }
        }
#endif
    }
}