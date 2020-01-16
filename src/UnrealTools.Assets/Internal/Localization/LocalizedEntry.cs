using UnrealTools.Core;

namespace UnrealTools.Assets.Internal.Localization
{
    public class LocalizedEntry
    {
        public FString LocResID { get; internal set; } = null!;
        public int SourceStringHash { get; internal set; }
        public FString LocalizedString { get; internal set; } = null!;

        public override string ToString() => LocalizedString.ToString();
    }
}
