namespace UETools.Assets.Enums
{
    /// <summary>
    /// Data versions for <see cref="LocMetaAsset">LocMeta</see> files.
    /// </summary>
    internal enum ELocMetaVersion : byte
    {
        /// <summary>
        /// Initial format.
        /// </summary>
        Initial = 0,

        LatestPlusOne,
        Latest = LatestPlusOne - 1
    }
}
