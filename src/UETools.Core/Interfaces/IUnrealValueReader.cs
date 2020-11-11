using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace UETools.Core.Interfaces
{
    /// <summary>
    /// <see cref="BinaryReader"/> like implementation of deserializing data from its binary form.
    /// </summary>
    public interface IUnrealValueReader : IDisposable
    {
        /// <summary>
        /// Length of the underlying binary data stream.
        /// </summary>
        long Length { get; }
        /// <summary>
        /// Byte offset position in the stream.
        /// </summary>
        long Position { get; set; }

        /// <summary>
        /// Sets the position within the current stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <returns>The new position within the current stream.</returns>
        long Seek(long offset, SeekOrigin origin);

        /// <summary>
        /// Reads <see cref="Memory{T}"/> of bytes out of the underlying data stream.
        /// </summary>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>Deserialized <see cref="Memory{T}"/> of bytes.</returns>
        Memory<byte> ReadBytes(int count);
        /// <summary>
        /// Reads <see cref="Span{T}"/> of bytes out of the underlying data stream.
        /// </summary>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>Deserialized <see cref="Span{T}"/> of bytes.</returns>
        Span<byte> ReadSpanBytes(int count);
        /// <summary>
        /// Reads <see langword="bool"/> value out of the underlying data stream.
        /// </summary>
        /// <returns>Deserialized <see langword="bool"/> value.</returns>
        bool ReadBoolean();
        /// <summary>
        /// Reads <see langword="sbyte"/> value out of the underlying data stream.
        /// </summary>
        /// <returns>Deserialized <see langword="sbyte"/> value.</returns>
        sbyte ReadSByte();
        /// <summary>
        /// Reads <see langword="short"/> value out of the underlying data stream.
        /// </summary>
        /// <returns>Deserialized <see langword="short"/> value.</returns>
        short ReadInt16();
        /// <summary>
        /// Reads <see langword="int"/> value out of the underlying data stream.
        /// </summary>
        /// <returns>Deserialized <see langword="int"/> value.</returns>
        int ReadInt32();
        /// <summary>
        /// Reads <see langword="long"/> value out of the underlying data stream.
        /// </summary>
        /// <returns>Deserialized <see langword="long"/> value.</returns>
        long ReadInt64();
        /// <summary>
        /// Reads <see langword="byte"/> value out of the underlying data stream.
        /// </summary>
        /// <returns>Deserialized <see langword="byte"/> value.</returns>
        byte ReadByte();
        /// <summary>
        /// Reads <see langword="ushort"/> value out of the underlying data stream.
        /// </summary>
        /// <returns>Deserialized <see langword="ushort"/> value.</returns>
        ushort ReadUInt16();
        /// <summary>
        /// Reads <see langword="uint"/> value out of the underlying data stream.
        /// </summary>
        /// <returns>Deserialized <see langword="uint"/> value.</returns>
        uint ReadUInt32();
        /// <summary>
        /// Get reader starting at specific <paramref name="start"/> offset.
        /// </summary>
        /// <param name="start">Offset at which the stream should start from the original stream.</param>
        /// <returns>Reader of the selected part of the stream.</returns>
        IUnrealValueReader Slice(long start);
        /// <summary>
        /// Get reader starting at specific <paramref name="start"/> offset, with specified <paramref name="length"/>.
        /// </summary>
        /// <param name="start">Offset at which the stream should start from the original stream.</param>
        /// <param name="length">Length of the sliced stream from the original stream.</param>
        /// <returns>Reader of the selected part of the stream.</returns>
        IUnrealValueReader Slice(long start, long length);

        /// <summary>
        /// Reads <see langword="ulong"/> value out of the underlying data stream.
        /// </summary>
        /// <returns>Deserialized <see langword="ulong"/> value.</returns>
        ulong ReadUInt64();
        /// <summary>
        /// Reads <see langword="float"/> value out of the underlying data stream.
        /// </summary>
        /// <returns>Deserialized <see langword="float"/> value.</returns>
        float ReadSingle();
        /// <summary>
        /// Reads <see langword="double"/> value out of the underlying data stream.
        /// </summary>
        /// <returns>Deserialized <see langword="double"/> value.</returns>
        double ReadDouble();
        /// <summary>
        /// Reads <see langword="decimal"/> value out of the underlying data stream.
        /// </summary>
        /// <returns>Deserialized <see langword="decimal"/> value.</returns>
        decimal ReadDecimal();
        /// <summary>
        /// Reads <see langword="string"/> value out of the underlying data stream, prefixed with single byte of length.
        /// </summary>
        /// <returns>Deserialized <see langword="string"/> value.</returns>
        string ReadByteString();
        /// <summary>
        /// Reads underlying <see langword="string"/> value of <see cref="FString"/> instance out of the underlying data stream.
        /// </summary>
        /// <param name="length">Negative length denotes <see cref="UnicodeEncoding"/>, positive <see cref="UTF8Encoding"/>.</param>
        /// <returns></returns>
        string ReadUnrealString(int length);

        bool FindElement(string tag, [NotNullWhen(true)] out DataSegment? element);
    }
}
