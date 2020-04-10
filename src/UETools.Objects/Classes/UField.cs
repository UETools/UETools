using UETools.Core;
using UETools.Core.Enums;
using UETools.Objects.Package;

namespace UETools.Objects.Classes
{
    class UField : UObject
    {
        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            if (reader.Version > UE4Version.VER_UE4_AUTOMATIC_VERSION)
            {
                reader.Read(out _next);
            }
        }

        private ObjectReference? _next;
    }
}
