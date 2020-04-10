namespace UETools.Assets.Enums
{
    /// <summary>
    /// Data versions for <see cref="LocResAsset">LocRes</see> files.
    /// </summary>
    internal enum ELocResVersion : byte
    {
        /// <summary>
        /// Legacy format file.
        /// </summary>
        /// <remarks>Will be missing the magic number.</remarks>
        Legacy = 0,
        /// <summary>
        /// Compact format file.
        /// </summary>
        /// <remarks>Strings are stored in a LUT to avoid duplication.</remarks>
        Compact = 1,
        /// <summary>
        /// Namespaces/keys are pre-hashed, we know the number of elements up-front, and the number of references for each string in the LUT (to allow stealing).
        /// </summary>
        Optimized = 2,

        LatestPlusOne,
        Latest = LatestPlusOne - 1
    }
}
