using System.Collections.Generic;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Asset
{
    internal class LocMetadataObject : IUnrealDeserializable
    {
        public void Deserialize(FArchive reader) => reader.Read(out _values);

        private Dictionary<FString, LocMetadataValue> _values = null!;
    }
}