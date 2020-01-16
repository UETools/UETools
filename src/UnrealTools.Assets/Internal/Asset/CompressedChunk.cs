using System;
using System.Collections.Generic;
using System.Text;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Assets.Internal.Asset
{
    internal class CompressedChunk : IUnrealDeserializable
    {
        public int UncompressedOffset { get => _uncompressedOffset; set => _uncompressedOffset = value; }
        public int UncompressedSize { get => _uncompressedSize; set => _uncompressedSize = value; }
        public int CompressedOffset { get => _compressedOffset; set => _compressedOffset = value; }
        public int CompressedSize { get => _compressedSize; set => _compressedSize = value; }

        public void Deserialize(FArchive reader)
        {
            reader.Read(out _uncompressedOffset);
            reader.Read(out _uncompressedSize);
            reader.Read(out _compressedOffset);
            reader.Read(out _compressedSize);
        }

        private int _uncompressedOffset;
        private int _uncompressedSize;
        private int _compressedOffset;
        private int _compressedSize;
    }
}
