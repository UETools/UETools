namespace UETools.Core.Interfaces
{
    /// <summary>
    /// Provides an interface for deserialization of the Unreal Engine binary data to implementing object.
    /// </summary>
    public interface IUnrealSerializable
    {
        /// <summary>
        /// Serialize  object with specified <paramref name="archive"/>.
        /// </summary>
        /// <param name="archive">Stream of binary data.</param>
        FArchive Serialize(FArchive archive);
    }
}
