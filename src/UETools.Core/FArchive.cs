using System;
using System.Buffers;
using System.IO;
using System.Threading.Tasks;
using UETools.Core.Enums;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    /// <summary>
    /// Representation of Unreal Engine 4 binary data stream reader/writer
    /// </summary>
    public partial class FArchive : IDisposable
    {
        public FArchive FindParent() => Parent is null ? this : Parent.FindParent();
        private FArchive? Parent { get; set; }
        /// <summary>
        /// Version of the asset, separate from serialized UObject <see cref="Version"/>.
        /// </summary>
        public int AssetVersion { get; set; }
        /// <summary>
        /// Subversion of the asset.
        /// </summary>
        /// <remarks>Use only for backwards incompatible changes checks.</remarks>
        public int AssetSubversion { get; set; }
        /// <summary>
        /// Version of the package being read.
        /// </summary>
        public UE4Version Version { get; set; }
        /// <summary>
        /// Licensee specific version of the package being read.
        /// </summary>
        public int LicenseeVersion { get; set; }

        // Some constructors?
        public FArchive(Memory<byte> memory) : this(new MemoryReader(memory)) { }
        public FArchive(IMemoryOwner<byte> memory) : this(new MemoryReader(memory)) { }
        public FArchive(DataSegment memory) : this(new MemoryReader(memory)) { }
        public FArchive(IUnrealValueReader reader) : this(reader, true) { }

        private FArchive(Memory<byte> memory, bool ownsStream) : this(new MemoryReader(memory), ownsStream) { }
        private FArchive(IUnrealValueReader reader, bool ownsStream)
        {
            Stream = reader;
            OwnsStream = ownsStream;
        }

        /// <summary>
        /// Sets the position within the current stream counting from <see cref="SeekOrigin.Current"/> by <paramref name="len"/> bytes.
        /// </summary>
        /// <param name="len">Count of bytes to move stream forward.</param>
        /// <returns>The new position within the current stream.</returns>
        public long Skip(long len) => Stream.Seek(len, SeekOrigin.Current);
        /// <summary>
        /// Sets the position within the current stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <returns>The new position within the current stream.</returns>
        public long Seek(long offset, SeekOrigin origin) => Stream.Seek(offset, origin);
        /// <summary>
        /// Sets the position within the current stream counting from <see cref="SeekOrigin.Begin"/>.
        /// </summary>
        /// <param name="offset">A byte offset relative to the start of the stream.</param>
        /// <returns>The new position within the current stream.</returns>
        public long Seek(long offset) => Seek(offset, SeekOrigin.Begin);
        /// <summary>
        /// Gets current position in the data stream.
        /// </summary>
        /// <returns>Current position in the data stream.</returns>
        public long Tell() => Stream.Position;
        /// <summary>
        /// Gets the length of the stream in bytes.
        /// </summary>
        /// <returns>Length of the stream in bytes.</returns>
        public long Length() => Stream.Length;
        /// <summary>
        /// Checks whether we've reached the end of the stream.
        /// </summary>
        /// <returns><see langword="true"/> if <see cref="Length()"/> is greater than current stream position; otherwise <see langword="false"/>.</returns>
        public bool EOF() => Tell() >= Length();

        public FArchive Slice(long offset) => new FArchive(Stream.Slice(offset), false)
        {
            Parent = this,
            Tables = this.Tables,
            Version = this.Version,
            AssetVersion = this.AssetVersion,
            AssetSubversion = this.AssetSubversion,
            Localization = this.Localization,
        };
        public FArchive SubStream(int length) => new FArchive(Stream.ReadBytes(length))
        {
            Parent = this,
            Tables = this.Tables,
            Version = this.Version,
            AssetVersion = this.AssetVersion,
            AssetSubversion = this.AssetSubversion,
            Localization = this.Localization,
        };
        public FArchive SubStream(long offset, int length)
        {
            Stream.Seek(offset, SeekOrigin.Begin);
            return SubStream(length);
        }

        /// <summary>
        /// Disposes underlying data stream.
        /// </summary>
        public void Dispose()
        {
            if (OwnsStream)
            {
                Stream.Dispose();
            }
        }

        private IUnrealValueReader Stream { get; set; }
        private bool OwnsStream { get; }
    }
}
