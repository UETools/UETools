using System.Collections.Generic;
using UnrealTools.Core;

namespace UnrealTools.Assets.Interfaces.Generic
{
    public interface IAssetData<T> where T : notnull
    {
        T ObjectPath { get; }
        T PackagePath { get; }
        T AssetClass { get; }
        T PackageName { get; }
        T AssetName { get; }
        Dictionary<T, FString> TagsAndValues { get; }

        List<int> ChunkIDs { get; }
        EPackageFlags PackageFlags { get; }
    }
}
