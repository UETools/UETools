using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Assets.Internal.Registry
{
    [DebuggerDisplay("{PackageName.Name.ToString()}")]
    public partial class AssetIdentifier : IUnrealDeserializable
    {
        [DisallowNull]
        public FName? PackageName
        {
            get => _packageName;
            set
            {
                _packageName = value;
                _fieldBits |= IdentifierField.PackageName;
            }
        }
        [DisallowNull]
        public FName? PrimaryAssetType
        {
            get => _primaryAssetType;
            set
            {
                _primaryAssetType = value;
                _fieldBits |= IdentifierField.AssetType;
            }
        }
        [DisallowNull]
        public FName? ObjectName
        {
            get => _objectName;
            set
            {
                _objectName = value;
                _fieldBits |= IdentifierField.ObjectName;
            }
        }
        [DisallowNull]
        public FName? ValueName
        {
            get => _valueName;
            set
            {
                _valueName = value;
                _fieldBits |= IdentifierField.ValueName;
            }
        }

        public void Deserialize(FArchive reader)
        {
            reader.ReadUnsafe(out _fieldBits);

            if ((_fieldBits & IdentifierField.PackageName) != 0)
                reader.Read(out _packageName);
            if ((_fieldBits & IdentifierField.AssetType) != 0)
                reader.Read(out _primaryAssetType);
            if ((_fieldBits & IdentifierField.ObjectName) != 0)
                reader.Read(out _objectName);
            if ((_fieldBits & IdentifierField.ValueName) != 0)
                reader.Read(out _valueName);
        }

        private IdentifierField _fieldBits;
        private FName? _packageName;
        private FName? _primaryAssetType;
        private FName? _objectName;
        private FName? _valueName;
    }
}
