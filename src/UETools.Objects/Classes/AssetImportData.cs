using UETools.Core;
using UETools.Objects.Classes.Internal;

namespace UETools.Objects.Classes
{
    class AssetImportData : UObject
    {
        public override FArchive Serialize(FArchive archive)
        {
            FString? str = default;
            archive.Read(ref str);
            return base.Serialize(archive);
        }
    }
}
