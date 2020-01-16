using UnrealTools.Core;

namespace UnrealTools.Objects.Classes
{
    public class UObject : TaggedObject
    {
        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _wasKill);
        }

        private bool _wasKill;
    }
}
