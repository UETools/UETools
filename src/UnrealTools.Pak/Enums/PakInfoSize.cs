namespace UnrealTools.Pak.Enums
{
    internal enum PakInfoSize
    {
        // sizeof(Magic) + sizeof(Version) + sizeof(IndexOffset) + sizeof(IndexSize) + sizeof(IndexHash) + sizeof(EncryptedIndex) + sizeof(Guid)
        Size = sizeof(uint) + sizeof(PakVersion) + sizeof(long) + sizeof(long) + 20 + sizeof(byte) + 16,
        // UE4.21
        Sizev8 = Size + (32 * 4),
        // UE4.23 pak version was not incremented with backwards incompatible change
        Sizev8a = Sizev8 + (32 * 5),
    }
}
