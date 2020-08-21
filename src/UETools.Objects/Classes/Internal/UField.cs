using UETools.Core;
using UETools.Core.Enums;
using UETools.Objects.Package;

namespace UETools.Objects.Classes.Internal
{
    // TODO: Fix
    class UField : UObject
    {
        public override FArchive Serialize(FArchive reader)
        {
            base.Serialize(reader);
            if (reader.Version > UE4Version.VER_UE4_AUTOMATIC_VERSION)
            {
                reader.Read(ref _next);
            }
            return reader;
        }

        private ObjectReference? _next;
    }
}
