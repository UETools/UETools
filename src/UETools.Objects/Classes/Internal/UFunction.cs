using UETools.Core;
using UETools.Core.Enums;
using UETools.Objects.Enums;
using UETools.Objects.Package;

namespace UETools.Objects.Classes.Internal
{
    internal class UFunction : UStruct
    {
        public override FArchive Serialize(FArchive reader)
        {
            base.Serialize(reader)
                .ReadUnsafe(ref _functionFlags);

            if ((_functionFlags & EFunctionFlags.Net) != 0)
                reader.Read(ref _repOffset);

            if (reader.Version >= UE4Version.VER_UE4_SERIALIZE_BLUEPRINT_EVENTGRAPH_FASTCALLS_IN_UFUNCTION)
            {
                reader.Read(ref _eventGraphFunction)
                      .Read(ref _eventGraphCallOffset);
            }
            return reader;
        }


        private EFunctionFlags _functionFlags;
        private ResolvedObjectReference<UFunction> _eventGraphFunction = null!;
        private int _eventGraphCallOffset;
        private short _repOffset;
    }
}
