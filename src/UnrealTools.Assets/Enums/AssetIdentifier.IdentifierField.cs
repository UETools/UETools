using System;

namespace UnrealTools.Assets.Internal.Registry
{
    partial class AssetIdentifier
    {
        [Flags]
        private enum IdentifierField : byte
        {
            None = 0,
            PackageName = 1,
            AssetType = 2,
            ObjectName = 4,
            ValueName = 8
        }
    }
}
