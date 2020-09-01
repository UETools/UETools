using UETools.Core;

namespace UETools.Objects.Classes.Internal
{
    public class UObject : TaggedObject
    {
        public override FArchive Serialize(FArchive archive) => base.Serialize(archive)
                                                                    .Read(ref _wasKill);

        private bool _wasKill;
    }
}
