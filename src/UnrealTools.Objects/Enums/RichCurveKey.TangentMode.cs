namespace UnrealTools.Objects.Structures
{
    partial struct RichCurveKey
    {
        /// <summary>Used only if using RCIM_Cubic.</summary>
        /// <remarks>
        /// If using RCIM_Cubic, this enum describes how the tangents should be controlled in editor.
        /// </remarks>
        enum TangentMode : byte
        {
            /// <summary>
            /// Automatically calculates tangents to create smooth curves between values.
            /// </summary>
            Auto,
            /// <summary>
            /// User specifies the tangent as a unified tangent where the two tangents are locked to each other, presenting a consistent curve before and after.
            /// </summary>
            User,
            /// <summary>
            /// User specifies the tangent as two separate broken tangents on each side of the key which can allow a sharp change in evaluation before or after.
            /// </summary>
            Break,
            /// <summary>
            /// No tangents.
            /// </summary>
            None,
        }
    }
}
