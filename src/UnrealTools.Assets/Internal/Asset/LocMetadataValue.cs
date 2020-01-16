using System;
using System.Collections.Generic;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Assets.Internal.Asset
{
    internal partial class LocMetadataValue : IUnrealDeserializable
    {
        public void Deserialize(FArchive reader)
        {
            reader.ReadUnsafe(out _metaDataType);
            switch (_metaDataType)
            {
                case ELocMetadataType.Boolean:
                    {
                        reader.Read(out bool b);
                    }
                    break;
                case ELocMetadataType.String:
                    {
                        reader.Read(out FString b);
                    }
                    break;
                case ELocMetadataType.Array:
                    {
                        reader.Read(out List<LocMetadataValue> ar);
                    }
                    break;
                case ELocMetadataType.Object:
                    {
                        reader.Read(out LocMetadataObject b);
                    }
                    break;
                default:
                    throw new NotImplementedException($"{_metaDataType} not implemented.");
            }
        }

        ELocMetadataType _metaDataType;
    }
}