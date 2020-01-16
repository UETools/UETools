using System;

namespace UnrealTools.Pak.Enums
{
    /// <summary>
    /// Flags controlling compression and decompression.
    /// </summary>
    [Flags]
    internal enum ECompressionFlags : int
    {
        /// <summary>
        /// No compression.
        /// </summary>
        COMPRESS_None = 0x00,
        /// <summary>
        /// Compress with ZLIB.
        /// </summary>
        COMPRESS_ZLIB = 0x01,
        /// <summary>
        /// Compress with GZIP.
        /// </summary>
        COMPRESS_GZIP = 0x02,
        /// <summary>
        /// Compress with user defined callbacks.
        /// </summary>
        COMPRESS_Custom = 0x04,
        /// <summary>
        /// Prefer compression that compresses smaller.
        /// </summary>
        /// <remarks>Only valid for compression.</remarks>
        COMPRESS_BiasMemory = 0x10,
        /// <summary>
        /// Prefer compression that compresses faster.
        /// </summary>
        /// <remarks>Only valid for compression.</remarks>
        COMPRESS_BiasSpeed = 0x20,
    };
}