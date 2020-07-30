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
using UETools.Core;
using UETools.Pak.Interfaces;

namespace UETools.Pak
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
        public static void Decompress(byte[] source, byte[] destination, PakEntry entry) => Decompress(source, 0, source.Length, destination, 0, destination.Length, entry);
        public static void Decompress(byte[] source, int srcOffset, int srcCount, byte[] destination, int dstOffset, int dstCount, PakEntry entry)
        {
            using var mem = new MemoryStream(source, srcOffset, srcCount);
            DecompressEntry(mem, destination, dstOffset, dstCount, entry);
        }
        private static void DecompressEntry(MemoryStream mem, byte[] destination, int dstOffset, int dstCount, PakEntry entry, int progress = 0)
        {
            if (entry.IsCompressed is false)
            {
                var size = (int)entry.Size;
                mem.ReadCount(destination, dstOffset + progress, dstCount - progress, size);
                progress += size;
            }
            else
            {
                foreach (var block in entry.CompressionBlocks)
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
                    mem.Seek(block.End, SeekOrigin.Begin);
                }
            }
            if(entry.LinkedEntry is PakEntry linked)
            {
                DecompressEntry(mem, destination, dstOffset, dstCount, linked, progress);
            }
        }

        public static async ValueTask<IMemoryOwner<byte>> DecompressAsync(Memory<byte> compressed, PakEntry entry, CancellationToken cancellationToken = default)
        {
            var decompressed = PakMemoryPool.Shared.Rent((int)entry.TotalUncompressedSize);
            if (MemoryMarshal.TryGetArray<byte>(compressed, out var src) && MemoryMarshal.TryGetArray<byte>(decompressed.Memory, out var dst))
            {
                await DecompressAsync(src.Array!, src.Offset, src.Count, dst.Array!, dst.Offset, dst.Count, entry, cancellationToken).ConfigureAwait(false);
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
            await DecompressEntryAsync(mem, destination, dstOffset, dstCount, entry, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        private static async ValueTask DecompressEntryAsync(MemoryStream mem, byte[] destination, int dstOffset, int dstCount, PakEntry entry, int progress = 0, CancellationToken cancellationToken = default)
        {
            if (entry.IsCompressed is false)
            {
                var size = (int)entry.Size;
                await mem.ReadCountAsync(destination, dstOffset + progress, dstCount - progress, size, cancellationToken).ConfigureAwait(false);
                progress = size;
            }
            else
            {
                foreach (var block in entry.CompressionBlocks)
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
            if (entry.LinkedEntry is PakEntry linked)
            {
                await DecompressEntryAsync(mem, destination, dstOffset, dstCount, linked, progress).ConfigureAwait(false);
            }
        }
    }
}
