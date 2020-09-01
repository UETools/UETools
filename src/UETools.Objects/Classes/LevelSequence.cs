using UETools.Core;
using UETools.Objects.Classes.Internal;

namespace UETools.Objects.Classes
{
    sealed class LevelSequence : UObject
    {
        public override FArchive Serialize(FArchive archive)
        {
            return archive;
            base.Serialize(archive);
        }
    }
}
