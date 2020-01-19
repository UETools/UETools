using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace UnrealTools.Core
{
    public static class StreamExtensions
    {
        public static void ReadWholeBuf(this Stream stream, Span<byte> buf)
        {
            int remaining = buf.Length;
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
        public static void ReadWholeBuf(this Stream stream, byte[] buf)
        {
            int remaining = buf.Length;
            var offset = 0;
            while (remaining > 0)
            {
                var read = stream.Read(buf, offset, remaining);
                if (read <= 0)
                    throw new EndOfStreamException($"End of stream reached, {remaining} bytes left to read");

                remaining -= read;
                offset += read;
            }
        }
        public static void ReadWholeBuf(this Stream stream, long offset, in Span<byte> buf) => stream.ReadWholeBuf(offset, SeekOrigin.Begin, buf);
        public static void ReadWholeBuf(this Stream stream, long offset, SeekOrigin origin, in Span<byte> buf)
        {
            stream.Seek(offset, origin);
            stream.ReadWholeBuf(buf);
        }

        public static async ValueTask ReadWholeBufAsync(this Stream stream, Memory<byte> buf, CancellationToken cancellationToken = default)
        {
            int remaining = buf.Length;
            var offset = 0;
            while (remaining > 0)
            {
                var task = stream.ReadAsync(buf.Slice(offset), cancellationToken);
                var read = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
                if (read <= 0)
                    throw new EndOfStreamException($"End of stream reached, {remaining} bytes left to read");

                remaining -= read;
                offset += read;

            }
        }
        public static ValueTask ReadWholeBufAsync(this Stream stream, long offset, in Memory<byte> buf, CancellationToken cancellationToken = default) => stream.ReadWholeBufAsync(offset, SeekOrigin.Begin, buf, cancellationToken);
        public static ValueTask ReadWholeBufAsync(this Stream stream, long offset, SeekOrigin origin, in Memory<byte> buf, CancellationToken cancellationToken = default)
        {
            stream.Seek(offset, SeekOrigin.Begin);
            return stream.ReadWholeBufAsync(buf, cancellationToken);
        }
    }
}