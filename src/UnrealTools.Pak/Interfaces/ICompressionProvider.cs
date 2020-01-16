using System;
using System.Collections.Generic;
using System.Text;
using UnrealTools.Core;

namespace UnrealTools.Pak.Interfaces
{
    interface ICompressionProvider
    {
        void Compress(Span<byte> data, Span<byte> buffer);
        void Decompress(Span<byte> data, Span<byte> buffer, IEnumerable<PakCompressedBlock> compressionBlocks);
    }
}
