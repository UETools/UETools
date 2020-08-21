using UETools.Core;
using UETools.Objects.Classes.Internal;

namespace UETools.Objects.Classes
{
    class AssetImportData : UObject
    {
        public override FArchive Serialize(FArchive reader)
        {
            FString? str = default;
            reader.Read(ref str);
            return base.Serialize(reader);
        }
    }
}
