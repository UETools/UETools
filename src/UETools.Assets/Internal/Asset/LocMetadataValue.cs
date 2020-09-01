using System;
using System.Collections.Generic;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Asset
{
    internal partial class LocMetadataValue : IUnrealSerializable
    {
        // TODO: FIX
        public FArchive Serialize(FArchive archive)
        {
            archive.ReadUnsafe(ref _metaDataType);
            switch (_metaDataType)
            {
                case ELocMetadataType.Boolean:
                    {
                        bool b = false;
                        archive.Read(ref b);
                    }
                    break;
                case ELocMetadataType.String:
                    {
                        FString? b = default;
                        archive.Read(ref b);
                    }
                    break;
                case ELocMetadataType.Array:
                    {
                        List<LocMetadataValue>? ar = default;
                        archive.Read(ref ar);
                    }
                    break;
                case ELocMetadataType.Object:
                    {
                        LocMetadataObject? b = default;
                        archive.Read(ref b);
                    }
                    break;
                default:
                    throw new NotImplementedException($"{_metaDataType} not implemented.");
            }
            return archive;
        }

        ELocMetadataType _metaDataType;
    }
}