using System.Collections.Generic;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Asset
{
    internal class LocMetadataObject : IUnrealSerializable
    {
        public FArchive Serialize(FArchive reader) => reader.Read(ref _values);

        private Dictionary<FString, LocMetadataValue> _values = null!;
    }
}