using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UETools.Core.Interfaces;
using UETools.TypeFactory;

namespace UETools.Core
{
    public partial class FArchive
    {
        /// <summary>
        /// Read value of type <typeparamref name="T"/> from stream.
        /// </summary>
        /// <typeparam name="T">Value type to read from the stream.</typeparam>
        /// <returns>Value read from stream.</returns>
        public T Read<T>() where T : unmanaged
            => MemoryMarshal.Read<T>(Stream.ReadSpanBytes(Unsafe.SizeOf<T>()));

        /// <summary>
        /// Reads instance of the unmanaged type <typeparamref name="T"/> from the data stream.
        /// </summary>
        /// <typeparam name="T">Value type to read from the stream.</typeparam>
        /// <param name="item">Object to initialize.</param>
        public FArchive ReadUnsafe<T>(ref T item) where T : unmanaged
        {
            item = MemoryMarshal.Read<T>(Stream.ReadSpanBytes(Unsafe.SizeOf<T>()));
            return this;
        }
        /// <summary>
        /// Creates new instance of the object and deserializes it from the data stream.
        /// </summary>
        /// <typeparam name="T">Any type implementing <see cref="IUnrealSerializable"/> and has parameterless constructor.</typeparam>
        /// <param name="item">Object to initialize.</param>
        public FArchive Read<T>([NotNull] ref T item) where T : IUnrealSerializable?, new()
        {
            item = Factory<T>.CreateInstance();
            item.Serialize(this);
            return this;
        }

        /// <summary>
        /// Reads <see langword="bool"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public FArchive Read(ref bool item)
        {
            item = Stream.ReadBoolean();
            return this;
        }
        /// <summary>
        /// Reads <see langword="sbyte"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public FArchive Read(ref sbyte item)
        {
            item = Stream.ReadSByte();
            return this;
        }
        /// <summary>
        /// Reads <see langword="short"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public FArchive Read(ref short item)
        {
            item = Stream.ReadInt16();
            return this;
        }
        /// <summary>
        /// Reads <see langword="int"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public FArchive Read(ref int item)
        {
            item = Stream.ReadInt32();
            return this;
        }
        /// <summary>
        /// Reads <see langword="long"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public FArchive Read(ref long item)
        {
            item = Stream.ReadInt64();
            return this;
        }
        /// <summary>
        /// Reads <see langword="byte"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public FArchive Read(ref byte item)
        {
            item = Stream.ReadByte();
            return this;
        }
        /// <summary>
        /// Reads <see langword="ushort"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public FArchive Read(ref ushort item)
        {
            item = Stream.ReadUInt16();
            return this;
        }
        /// <summary>
        /// Reads <see langword="uint"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public FArchive Read(ref uint item)
        {
            item = Stream.ReadUInt32();
            return this;
        }
        /// <summary>
        /// Reads <see langword="ulong"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public FArchive Read(ref ulong item)
        {
            item = Stream.ReadUInt64();
            return this;
        }
        /// <summary>
        /// Reads <see langword="Guid"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public FArchive Read(ref Guid item)
        {
            item = MemoryMarshal.Read<Guid>(Stream.ReadSpanBytes(16));
            return this;
        }
        /// <summary>
        /// Reads <see langword="float"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public FArchive Read(ref float item)
        {
            item = Stream.ReadSingle();
            return this;
        }
        /// <summary>
        /// Reads <see langword="double"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public FArchive Read(ref double item)
        {
            item = Stream.ReadDouble();
            return this;
        }
        /// <summary>
        /// Reads <see langword="decimal"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public FArchive Read(ref decimal item)
        {
            item = Stream.ReadDecimal();
            return this;
        }
        /// <summary>
        /// Reads <see langword="string"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        /// <param name="length"></param>
        internal FArchive Read([AllowNull] ref string item, int length)
        {
            item = Stream.ReadUnrealString(length);
            return this;
        }
        /// <summary>
        /// Reads <see cref="Array"/> of <see langword="byte"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public FArchive Read([AllowNull] ref byte[]?item) => Read(ref item, Read<int>());
        /// <summary>
        /// Reads <see cref="Array"/> of <see langword="byte"/> out of data stream, using declared <paramref name="length"/>.
        /// </summary>
        /// <param name="item">Value to read.</param>
        /// <param name="length">Number of bytes to read.</param>
        public FArchive Read([AllowNull] ref byte[] item, int length)
        {
            item = Stream.ReadBytes(length).ToArray();
            return this;
        }
        /// <summary>
        /// Reads <see cref="Span{T}"/> of <see langword="byte"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public FArchive Read(ref Span<byte> item) => Read(ref item, Read<int>());
        /// <summary>
        /// Reads <see cref="Span{T}"/> of <see langword="byte"/> out of data stream, using declared <paramref name="length"/>.
        /// </summary>
        /// <param name="item">Value to read.</param>
        /// <param name="length">Number of bytes to read.</param>
        public FArchive Read(ref Span<byte> item, int length)
        {
            item = Stream.ReadSpanBytes(length);
            return this;
        }
        /// <summary>
        /// Reads <see cref="Memory{T}"/> of <see langword="byte"/> out of data stream.
        /// </summary>
        /// <param name="item">Value to read.</param>
        public FArchive Read(ref Memory<byte> item) => Read(ref item, Read<int>());
        /// <summary>
        /// Reads <see cref="Memory{T}"/> of <see langword="byte"/> out of data stream, using declared <paramref name="length"/>.
        /// </summary>
        /// <param name="item">Value to read.</param>
        /// <param name="length">Number of bytes to read.</param>
        public FArchive Read(ref Memory<byte> item, int length)
        {
            item = Stream.ReadBytes(length);
            return this;
        }
    }
}
