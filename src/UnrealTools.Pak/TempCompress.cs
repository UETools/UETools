using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnrealTools.Core;
using UnrealTools.Pak.Interfaces;

namespace UnrealTools.Pak
{
    class TempCompress : ICompressionProvider
    {
        public int Method { get; internal set; }

        public void Compress(Span<byte> data, Span<byte> buffer)
        {
            var mem = new MemoryStream(data.ToArray());
            var stream = Method switch
            {
                1 => new Ionic.Zlib.ZlibStream(mem, Ionic.Zlib.CompressionMode.Compress, true),
                2 => new Ionic.Zlib.GZipStream(mem, Ionic.Zlib.CompressionMode.Compress, true),
                _ => Stream.Null
            };
            stream.ReadWholeBuf(buffer);
        }

        public void Decompress(Span<byte> data, Span<byte> buffer, IEnumerable<PakCompressedBlock> compressionBlocks)
        {
            const int blockSize = 0x10000;
            using var mem = new MemoryStream(data.ToArray());
            int i = 0;
            foreach(var block in compressionBlocks)
            {
                mem.Seek(block.Start, SeekOrigin.Begin);
                using var stream = Method switch
                {
                    1 => new Ionic.Zlib.ZlibStream(mem, Ionic.Zlib.CompressionMode.Decompress, true),
                    2 => new Ionic.Zlib.GZipStream(mem, Ionic.Zlib.CompressionMode.Decompress, true),
                    _ => Stream.Null
                };
                var target = buffer.Slice(blockSize * i, Math.Min(blockSize, buffer.Length - (blockSize * i)));
                stream.ReadWholeBuf(target);
                i++;
            }
            //stream.ReadWholeBuf(buffer);
        }
    }
}
