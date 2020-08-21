using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Asset
{
    internal class CompressedChunk : IUnrealSerializable
    {
        public int UncompressedOffset { get => _uncompressedOffset; set => _uncompressedOffset = value; }
        public int UncompressedSize { get => _uncompressedSize; set => _uncompressedSize = value; }
        public int CompressedOffset { get => _compressedOffset; set => _compressedOffset = value; }
        public int CompressedSize { get => _compressedSize; set => _compressedSize = value; }

        public FArchive Serialize(FArchive reader)
            => reader.Read(ref _uncompressedOffset)
                     .Read(ref _uncompressedSize)
                     .Read(ref _compressedOffset)
                     .Read(ref _compressedSize);

        private int _uncompressedOffset;
        private int _uncompressedSize;
        private int _compressedOffset;
        private int _compressedSize;
    }
}
