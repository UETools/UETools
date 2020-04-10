namespace UETools.Core.Enums
{
    internal enum RoundingMode : byte
    {
        /** Rounds to the nearest place, equidistant ties go to the value which is closest to an even value: 1.5 becomes 2, 0.5 becomes 0 */
        HalfToEven,
        /** Rounds to nearest place, equidistant ties go to the value which is further from zero: -0.5 becomes -1.0, 0.5 becomes 1.0 */
        HalfFromZero,
        /** Rounds to nearest place, equidistant ties go to the value which is closer to zero: -0.5 becomes 0, 0.5 becomes 0. */
        HalfToZero,
        /** Rounds to the value which is further from zero, "larger" in absolute value: 0.1 becomes 1, -0.1 becomes -1 */
        FromZero,
        /** Rounds to the value which is closer to zero, "smaller" in absolute value: 0.1 becomes 0, -0.1 becomes 0 */
        ToZero,
        /** Rounds to the value which is more negative: 0.1 becomes 0, -0.1 becomes -1 */
        ToNegativeInfinity,
        /** Rounds to the value which is more positive: 0.1 becomes 1, -0.1 becomes 0 */
        ToPositiveInfinity,


        // Add new enum types at the end only! They are serialized by index.
    };
}