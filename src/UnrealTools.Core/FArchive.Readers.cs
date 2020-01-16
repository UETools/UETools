using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnrealTools.Core.Interfaces;
using UnrealTools.TypeFactory;

namespace UnrealTools.Core
{
    public partial class FArchive
    {
        /// <summary>
        /// Reads instance of the unmanaged type <typeparamref name="T"/> from the data stream.
        /// </summary>
        /// <typeparam name="T">Value type to read from the stream.</typeparam>
        /// <param name="item">Object to initialize.</param>
        public T ReadUnsafe<T>(out T item) where T : unmanaged
            => item = MemoryMarshal.Read<T>(Stream.ReadSpanBytes(Unsafe.SizeOf<T>()));
        /// <summary>
        /// Creates new instance of the object and deserializes it from the data stream.
        /// </summary>
        /// <typeparam name="T">Any type implementing <see cref="IUnrealDeserializable"/> and has parameterless constructor.</typeparam>
        /// <param name="item">Object to initialize.</param>
        public T Read<T>(out T item) where T : IUnrealDeserializable?, new()
        {
            item = Factory<T>.CreateInstance();
            item.Deserialize(this);
            return item;
        }

        /// <summary>
        /// Reads <see langword="bool"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public bool Read(out bool item) => item = Stream.ReadBoolean();
        /// <summary>
        /// Reads <see langword="sbyte"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public sbyte Read(out sbyte item) => item = Stream.ReadSByte();
        /// <summary>
        /// Reads <see langword="short"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public short Read(out short item) => item = Stream.ReadInt16();
        /// <summary>
        /// Reads <see langword="int"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public int Read(out int item) => item = Stream.ReadInt32();
        /// <summary>
        /// Reads <see langword="long"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public long Read(out long item) => item = Stream.ReadInt64();
        /// <summary>
        /// Reads <see langword="byte"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public byte Read(out byte item) => item = Stream.ReadByte();
        /// <summary>
        /// Reads <see langword="ushort"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public ushort Read(out ushort item) => item = Stream.ReadUInt16();
        /// <summary>
        /// Reads <see langword="uint"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public uint Read(out uint item) => item = Stream.ReadUInt32();
        /// <summary>
        /// Reads <see langword="ulong"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public ulong Read(out ulong item) => item = Stream.ReadUInt64();
        /// <summary>
        /// Reads <see langword="Guid"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public Guid Read(out Guid item) => item = MemoryMarshal.Read<Guid>(Stream.ReadSpanBytes(16));
        /// <summary>
        /// Reads <see langword="float"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public float Read(out float item) => item = Stream.ReadSingle();
        /// <summary>
        /// Reads <see langword="double"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public double Read(out double item) => item = Stream.ReadDouble();
        /// <summary>
        /// Reads <see langword="decimal"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public decimal Read(out decimal item) => item = Stream.ReadDecimal();
        /// <summary>
        /// Reads <see langword="string"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        /// <param name="length"></param>
        internal string Read(out string item, int length) => item = Stream.ReadUnrealString(length);
        /// <summary>
        /// Reads <see cref="Array"/> of <see langword="byte"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public byte[] Read(out byte[] item) => Read(out item, Read(out int _));
        /// <summary>
        /// Reads <see cref="Array"/> of <see langword="byte"/> out of data stream, using declared <paramref name="length"/>.
        /// </summary>
        /// <param name="item">Value to read.</param>
        /// <param name="length">Number of bytes to read.</param>
        public byte[] Read(out byte[] item, int length) => item = Stream.ReadBytes(length).ToArray();
        /// <summary>
        /// Reads <see cref="Span{T}"/> of <see langword="byte"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public Span<byte> Read(out Span<byte> item) => Read(out item, Read(out int _));
        /// <summary>
        /// Reads <see cref="Span{T}"/> of <see langword="byte"/> out of data stream, using declared <paramref name="length"/>.
        /// </summary>
        /// <param name="item">Value to read.</param>
        /// <param name="length">Number of bytes to read.</param>
        public Span<byte> Read(out Span<byte> item, int length) => item = Stream.ReadSpanBytes(length);
        /// <summary>
        /// Reads <see cref="Memory{T}"/> of <see langword="byte"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public Memory<byte> Read(out Memory<byte> item) => Read(out item, Read(out int _));
        /// <summary>
        /// Reads <see cref="Memory{T}"/> of <see langword="byte"/> out of data stream, using declared <paramref name="length"/>.
        /// </summary>
        /// <param name="item">Value to read.</param>
        /// <param name="length">Number of bytes to read.</param>
        public Memory<byte> Read(out Memory<byte> item, int length) => item = Stream.ReadBytes(length);
    }
}
