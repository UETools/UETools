namespace UnrealTools.Pak.Enums
{
    internal enum PakVersion : int
    {
        Initial = 1,
        NoTimestamps = 2,
        CompressionEncryption = 3,
        IndexEncryption = 4,
        RelativeChunkOffsets = 5,
        DeleteRecords = 6,
        EncryptionKeyGuid = 7,
        FNameBasedCompressionMethod = 8,

        Last,
        Latest = Last - 1,
    };
}
