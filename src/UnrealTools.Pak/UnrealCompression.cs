using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnrealTools.Core;
using UnrealTools.Pak.Interfaces;

namespace UnrealTools.Pak
{
    internal class UnrealCompression
    {
        public static IMemoryOwner<byte> Decompress(Memory<byte> compressed, PakEntry entry)
        {
            var decompressed = PakMemoryPool.Shared.Rent((int)entry.TotalUncompressedSize);
            if (MemoryMarshal.TryGetArray<byte>(compressed, out var src) && MemoryMarshal.TryGetArray<byte>(decompressed.Memory, out var dst))
                Decompress(src.Array!, src.Offset, src.Count, dst.Array!, dst.Offset, dst.Count, entry);
            else
            {
                decompressed.Dispose();
                throw new NotImplementedException();
                //Decompress(data.Memory.ToArray(), 0, data.Memory.Length, buffer, compressionBlocks);
            }
            return decompressed;
        }
        public static void Decompress(byte[] source, int srcOffset, int srcCount, byte[] destination, int dstOffset, int dstCount, PakEntry entry)
        {
            using var mem = new MemoryStream(source, srcOffset, srcCount);
            var progress = 0;
            foreach (var block in entry.TotalBlocks)
            {
                mem.Seek(block.Start, SeekOrigin.Begin);
                using Stream stream = entry.CompressionMethod switch
                {
                    1 => new ZlibStream(mem, CompressionMode.Decompress, true),
                    2 => new GZipStream(mem, CompressionMode.Decompress, true),
                    _ => throw new NotImplementedException($"CompressionMethod '{entry.CompressionMethod}' not implemented")
                };
                var read = 0;
                do
                {
                    read = stream.Read(destination, dstOffset + progress, dstCount - progress);
                    progress += read;
                } while (read > 0);
            }
        }
        public static async ValueTask<IMemoryOwner<byte>> DecompressAsync(Memory<byte> compressed, PakEntry entry, CancellationToken cancellationToken = default)
        {
            var decompressed = PakMemoryPool.Shared.Rent((int)entry.TotalUncompressedSize);
            if (MemoryMarshal.TryGetArray<byte>(compressed, out var src) && MemoryMarshal.TryGetArray<byte>(decompressed.Memory, out var dst))
            {
                var task = DecompressAsync(src.Array!, src.Offset, src.Count, dst.Array!, dst.Offset, dst.Count, entry, cancellationToken);
                if (!task.IsCompletedSuccessfully)
                    await task.ConfigureAwait(false);
            }
            else
            {
                decompressed.Dispose();
                throw new NotImplementedException();
            }
            return decompressed;
        }
        public static async ValueTask DecompressAsync(byte[] source, int srcOffset, int srcCount, byte[] destination, int dstOffset, int dstCount, PakEntry entry, CancellationToken cancellationToken = default)
        {
            using var mem = new MemoryStream(source, srcOffset, srcCount);
            var progress = 0;
            foreach (var block in entry.TotalBlocks)
            {
                mem.Seek(block.Start, SeekOrigin.Begin);
                using Stream stream = entry.CompressionMethod switch
                {
                    1 => new ZlibStream(mem, CompressionMode.Decompress, true),
                    2 => new GZipStream(mem, CompressionMode.Decompress, true),
                    _ => throw new NotImplementedException($"CompressionMethod '{entry.CompressionMethod}' not implemented")
                };
                var read = 0;
                do
                {
                    read = await stream.ReadAsync(destination, dstOffset + progress, dstCount - progress, cancellationToken).ConfigureAwait(false);
                    progress += read;
                } while (read > 0);
            }
        }
    }
}
