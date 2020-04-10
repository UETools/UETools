namespace UETools.Objects.Structures
{
    partial struct RichCurveKey
    {
        /// <summary>
        /// Method of interpolation between this key and the next.
        /// </summary>
        enum InterpMode : byte
        {
            /// <summary>
            /// Use linear interpolation between values.
            /// </summary>
            Linear,
            /// <summary>
            /// Use a constant value. Represents stepped values.
            /// </summary>
            Constant,
            /// <summary>
            /// Cubic interpolation. See TangentMode for different cubic interpolation options.
            /// </summary>
            Cubic,
            /// <summary>
            /// No interpolation.
            /// </summary>
            None,
        }
    }
}
