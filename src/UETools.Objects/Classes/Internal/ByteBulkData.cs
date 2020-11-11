using System;
using System.Diagnostics;
using System.IO;
using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Enums;

namespace UETools.Objects.Classes
{
    internal class ByteBulkData : IUnrealSerializable
    {
        public byte[] Bytes => BulkData ?? Array.Empty<byte>();

        private FArchive SerializeHeader(FArchive archive) => archive.ReadUnsafe(ref _bulkDataFlags)
                                                                     .Read(ref _elementCount)
                                                                     .Read(ref _bulkDataSizeOnDisk)
                                                                     .Read(ref _bulkDataOffsetInFile);
        public FArchive Serialize(FArchive archive)
        {
            SerializeHeader(archive);
            if ((_bulkDataFlags & EBulkDataFlags.Unused) != 0 || _elementCount == 0)
            {
                BulkData = Array.Empty<byte>();
            }
            else
            {
                if ((_bulkDataFlags & (EBulkDataFlags.OptionalPayload | EBulkDataFlags.PayloadInSeperateFile)) != 0 && archive.FindTagSubstream((_bulkDataFlags & EBulkDataFlags.OptionalPayload) != 0 ? ".uptnl" : ".ubulk", out var tagData))
                {
                    tagData.Seek(_bulkDataOffsetInFile, SeekOrigin.Current);
                    SerializeData(tagData);
                }
                else if ((_bulkDataFlags & EBulkDataFlags.PayloadAtEndOfFile) != 0)
                {
                    throw new NotImplementedException($"{nameof(EBulkDataFlags.PayloadAtEndOfFile)} is not implemented yet!");
                }
                else if ((_bulkDataFlags & EBulkDataFlags.ForceInlinePayload) != 0)
                {
                    SerializeData(archive);
                }
            }
            return archive;
        }
        private void SerializeData(FArchive archive)
        {
            if ((_bulkDataFlags & EBulkDataFlags.SerializeCompressed) != 0)
            {
                throw new NotImplementedException("Texture compression is not supported yet!");
            }
            else
            {
                archive.Read(ref BulkData, _elementCount);
            }
        }

        EBulkDataFlags _bulkDataFlags;
        int _elementCount;
        long _bulkDataOffsetInFile;
        int _bulkDataSizeOnDisk;
        byte[] BulkData = null!; 
    }
}