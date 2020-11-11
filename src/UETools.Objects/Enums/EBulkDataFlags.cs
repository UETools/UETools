using System;
using System.Collections.Generic;
using System.Text;

namespace UETools.Objects.Enums
{
    [Flags]
    internal enum EBulkDataFlags
    {
        None = 0,
        PayloadAtEndOfFile = 1 << 0,
        SerializeCompressedZLIB = 1 << 1,
        ForceSingleElementSerialization = 1 << 2,
        SingleUse = 1 << 3,
        Unused = 1 << 5,
        ForceInlinePayload = 1 << 6,
        SerializeCompressed = (SerializeCompressedZLIB),
        ForceStreamPayload = 1 << 7,
        PayloadInSeperateFile = 1 << 8,
        SerializeCompressedBitWindow = 1 << 9,
        Force_NOT_InlinePayload = 1 << 10,
        OptionalPayload = 1 << 11,
        MemoryMappedPayload = 1 << 12,
        Size64Bit = 1 << 13,
        DuplicateNonOptionalPayload = 1 << 14,
        BadDataVersion = 1 << 15,
        UsesIoDispatcher = 1 << 16,
        DataIsMemoryMapped = 1 << 17
    }
}
