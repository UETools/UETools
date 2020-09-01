using UETools.Core;
using UETools.Core.Enums;
using UETools.Objects.Package;

namespace UETools.Objects.Classes.Internal
{
    // TODO: Fix
    class UField : UObject
    {
        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);
            if (archive.Version > UE4Version.VER_UE4_AUTOMATIC_VERSION)
            {
                archive.Read(ref _next);
            }
            return archive;
        }

        private ObjectReference? _next;
    }
}
