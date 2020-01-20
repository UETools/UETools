using UnrealTools.Core;

namespace UnrealTools.KismetVM.Enums
{
    internal enum EBlueprintTextLiteralType : byte
    {
        /// <summary>
        /// Text is an empty string.
        /// </summary>
        /// <remarks>The bytecode contains no strings, and you should use <see cref="FText.GetEmpty"/> to initialize the <see cref="FText"/> instance.</remarks>
        Empty,
        /// <summary>
        /// Text is localized.
        /// </summary>
        /// <remarks>The bytecode will contain three strings - source, key, and namespace - and should be loaded via FInternationalization</remarks>
        LocalizedText,
        /// <summary>
        /// Text is culture invariant.
        /// </summary>
        /// <remarks>The bytecode will contain one string, and you should use <see cref="FText.AsCultureInvariant"/> to initialize the <see cref="FText"/> instance.</remarks>
        InvariantText,
        /// <summary>
        /// Text is a literal FString.
        /// </summary>
        /// <remarks>The bytecode will contain one string, and you should use <see cref="FText.FromString"/> to initialize the <see cref="FText"/> instance.</remarks>
        LiteralString,
        /// <summary>
        /// Text is from a string table.
        /// </summary>
        /// <remarks>The bytecode will contain an object pointer (not used) and two strings - the table ID, and key - and should be found via <see cref="FText.FromStringTable"/>.</remarks>
        StringTableEntry,
    }
}
