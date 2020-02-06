using System.Collections.Generic;
using UnrealTools.Core;
using UnrealTools.Core.Enums;
using UnrealTools.Core.Interfaces;
using UnrealTools.Objects.Enums;
using UnrealTools.Objects.Package;

namespace UnrealTools.Objects.Classes
{
    internal class UClass : UStruct
    {
        class ImplementedInterface : IUnrealDeserializable
        {
            public void Deserialize(FArchive reader)
            {
                reader.Read(out _class);
                reader.Read(out _pointerOffset);
                reader.Read(out _implementedByK2);
            }

            private ObjectReference _class = null!;
            private int _pointerOffset;
            private bool _implementedByK2;
        }

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _funcMap);
            reader.ReadUnsafe(out _flags);

            if (reader.Version < UE4Version.VER_UE4_CLASS_NOTPLACEABLE_ADDED)
            {
                _flags ^= EClassFlags.NotPlaceable;
                if ((_flags & EClassFlags.NotPlaceable) == 0)
                    _flags |= EClassFlags.NotPlaceable;
            }

            reader.Read(out _classWithin);
            reader.Read(out _classConfigName);

            if (reader.Version < UE4Version.VER_UE4_UCLASS_SERIALIZE_INTERFACES_AFTER_LINKING)
                reader.Read(out _serializedInterface);

            reader.Read(out _deprecatedForceScriptOrder);
            reader.Read(out _dummy);

            if (reader.Version >= UE4Version.VER_UE4_ADD_COOKED_TO_UCLASS)
                reader.Read(out _cooked);
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
