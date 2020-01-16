using System.Collections.Generic;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Assets.Internal.Asset
{
    internal class LocMetadataObject : IUnrealDeserializable
    {
        public void Deserialize(FArchive reader) => reader.Read(out _values);

        private Dictionary<FString, LocMetadataValue> _values = null!;
    }
}