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
            public FArchive Serialize(FArchive reader) 
                => reader.Read(ref _class)
                         .Read(ref _pointerOffset)
                         .Read(ref _implementedByK2);

            private ObjectReference _class = null!;
            private int _pointerOffset;
            private bool _implementedByK2;
        }

        public override FArchive Serialize(FArchive reader)
        {
            base.Serialize(reader)
                .Read(ref _funcMap)
                .ReadUnsafe(ref _flags);

            if (reader.Version < UE4Version.VER_UE4_CLASS_NOTPLACEABLE_ADDED)
            {
                _flags ^= EClassFlags.NotPlaceable;
                if ((_flags & EClassFlags.NotPlaceable) == 0)
                    _flags |= EClassFlags.NotPlaceable;
            }

            reader.Read(ref _classWithin)
                  .Read(ref _classConfigName);

            if (reader.Version < UE4Version.VER_UE4_UCLASS_SERIALIZE_INTERFACES_AFTER_LINKING)
                reader.Read(ref _serializedInterface);

            reader.Read(ref _deprecatedForceScriptOrder)
                  .Read(ref _dummy);

            if (reader.Version >= UE4Version.VER_UE4_ADD_COOKED_TO_UCLASS)
                reader.Read(ref _cooked);

            return reader;
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
