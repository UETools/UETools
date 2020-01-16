namespace UnrealTools.Objects.Structures
{
    partial struct RichCurveKey
    {
        /// <summary>
        /// Enumerates tangent weight modes.
        /// </summary>
        enum TangentWeightMode : byte
        {
            /// <summary>
            /// Don't take tangent weights into account.
            /// </summary>
            WeightedNone,
            /// <summary>
            /// Only take the arrival tangent weight into account for evaluation.
            /// </summary>
            WeightedArrive,
            /// <summary>
            /// Only take the leaving tangent weight into account for evaluation.
            /// </summary>
            WeightedLeave,
            /// <summary>
            /// Take both the arrival and leaving tangent weights into account for evaluation.
            /// </summary>
            WeightedBoth,
        }
    }
}
