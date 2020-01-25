using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using UnrealTools.Core;

namespace UnrealTools.Pak.Interfaces
{
    interface ICompressionProvider
    {
        void Compress([DisallowNull] ref IMemoryOwner<byte>? data, Span<byte> buffer);
        void Decompress([DisallowNull] ref IMemoryOwner<byte>? data, Span<byte> buffer, IEnumerable<PakCompressedBlock> compressionBlocks);
    }
}
