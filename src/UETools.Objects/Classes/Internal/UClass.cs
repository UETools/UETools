using System.Collections.Generic;
using UETools.Core;
using UETools.Core.Enums;
using UETools.Core.Interfaces;
using UETools.Objects.Enums;
using UETools.Objects.Package;

namespace UETools.Objects.Classes.Internal
{
    internal class UClass : UStruct
    {
        class ImplementedInterface : IUnrealSerializable
        {
            public FArchive Serialize(FArchive archive)
                => archive.Read(ref _class)
                          .Read(ref _pointerOffset)
                          .Read(ref _implementedByK2);

            private ObjectReference _class = null!;
            private int _pointerOffset;
            private bool _implementedByK2;
        }

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive)
                .Read(ref _funcMap)
                .ReadUnsafe(ref _flags);

            if (archive.Version < UE4Version.VER_UE4_CLASS_NOTPLACEABLE_ADDED)
            {
                _flags ^= EClassFlags.NotPlaceable;
                if ((_flags & EClassFlags.NotPlaceable) == 0)
                    _flags |= EClassFlags.NotPlaceable;
            }

            archive.Read(ref _classWithin)
                   .Read(ref _classConfigName);

            if (archive.Version < UE4Version.VER_UE4_UCLASS_SERIALIZE_INTERFACES_AFTER_LINKING)
                archive.Read(ref _serializedInterface);

            archive.Read(ref _deprecatedForceScriptOrder)
                   .Read(ref _dummy);

            if (archive.Version >= UE4Version.VER_UE4_ADD_COOKED_TO_UCLASS)
                archive.Read(ref _cooked);

            return archive;
        }

        private Dictionary<FName, ResolvedObjectReference<UFunction>> _funcMap = null!;
        private EClassFlags _flags;
        private ObjectReference _classWithin = null!;
        private FName _classConfigName = null!;
        private List<ImplementedInterface>? _serializedInterface;
        private bool _deprecatedForceScriptOrder;
        private FName _dummy = null!;
        private bool _cooked;
    }
}
