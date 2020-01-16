namespace UnrealTools.Core.Interfaces
{
    /// <summary>
    /// Provides an interface for deserialization of the Unreal Engine binary data to implementing object.
    /// </summary>
    public interface IUnrealDeserializable
    {
        /// <summary>
        /// Deserialize to object from binary data.
        /// </summary>
        /// <param name="reader">Stream of binary data.</param>
        void Deserialize(FArchive reader);
    }
}
