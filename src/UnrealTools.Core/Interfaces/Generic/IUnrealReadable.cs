using System.IO;

namespace UnrealTools.Core.Interfaces
{
    /// <summary>
    /// Provides an interface for serializing object data as readable text.
    /// </summary>
    public interface IUnrealReadable<T> where T : TextWriter
    {
        /// <summary>
        /// Read object into the <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">Buffer to write to.</param>
        void ReadTo(T writer);
    }
}
