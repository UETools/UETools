using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnrealTools.Core;
using UnrealTools.Pak.Interfaces;

namespace UnrealTools.Pak
{
    class TempCompress : ICompressionProvider
    {
        public int Method { get; internal set; }

        public void Compress([DisallowNull] ref IMemoryOwner<byte>? data, Span<byte> buffer)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            var mem = new MemoryStream(data.Memory.Span.ToArray());
            var stream = Method switch
            {
                1 => new Ionic.Zlib.ZlibStream(mem, Ionic.Zlib.CompressionMode.Compress, true),
                2 => new Ionic.Zlib.GZipStream(mem, Ionic.Zlib.CompressionMode.Compress, true),
                _ => Stream.Null
            };
            stream.ReadWholeBuf(buffer);
            data.Dispose();
            data = null;
        }

        public void Decompress([DisallowNull] ref IMemoryOwner<byte>? data, Span<byte> buffer, IEnumerable<PakCompressedBlock> compressionBlocks)
        {
            if (data is null) throw new ArgumentNullException(nameof(data));

            if (MemoryMarshal.TryGetArray<byte>(data.Memory, out var segment))
                Decompress(segment.Array!, segment.Offset, segment.Count, buffer, compressionBlocks);
            else
                Decompress(data.Memory.ToArray(), 0, data.Memory.Length, buffer, compressionBlocks);

            data.Dispose();
            data = null;
        }

        private void Decompress(byte[] buffer, int offset, int count, Span<byte> outBuffer, IEnumerable<PakCompressedBlock> compressionBlocks)
        {
            const int blockSize = 0x10000;
            using var mem = new MemoryStream(buffer, offset, count);
            int i = 0;
            foreach (var block in compressionBlocks)
            {
                mem.Seek(block.Start, SeekOrigin.Begin);
                using var stream = Method switch
                {
                    1 => new Ionic.Zlib.ZlibStream(mem, Ionic.Zlib.CompressionMode.Decompress, true),
                    2 => new Ionic.Zlib.GZipStream(mem, Ionic.Zlib.CompressionMode.Decompress, true),
                    _ => Stream.Null
                };
                var target = outBuffer.Slice(blockSize * i, Math.Min(blockSize, outBuffer.Length - (blockSize * i)));
                stream.ReadWholeBuf(target);
                i++;
            }
        }
    }
}
