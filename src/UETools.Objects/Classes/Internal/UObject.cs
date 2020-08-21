using UETools.Core;

namespace UETools.Objects.Classes.Internal
{
    public class UObject : TaggedObject
    {
        public override FArchive Serialize(FArchive reader) => base.Serialize(reader)
                                                                   .Read(ref _wasKill);

        private bool _wasKill;
    }
}
