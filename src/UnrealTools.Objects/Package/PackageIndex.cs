using System;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Objects.Package
{
    public struct PackageIndex : IUnrealDeserializable, IEquatable<PackageIndex>, IEquatable<int>
    {
        public void Deserialize(FArchive reader) => reader.Read(out _value);

        internal ObjectResource? Resolve(FArchive reader)
        {
            if (_value > 0)
                return Export(reader, _value);
            else if (_value < 0)
                return Import(reader, _value);

            return null;
        }
        private static ObjectExport? Export(FArchive reader, int index)
        {
            if (reader.GetTable<ObjectExport>(ExportTable.TableName) is ExportTable Exports)
            {
                var i = index - 1;
                if (i < Exports.Items.Count)
                    return Exports.Items[i];
            }
            return null;
        }
        private static ObjectImport? Import(FArchive reader, int index)
        {
            if (reader.GetTable<ObjectImport>(ImportTable.TableName) is ImportTable Imports)
            {
                var i = -index - 1;
                if (i < Imports.Items.Count)
                    return Imports.Items[i];
            }
            return null;
        }

        public override bool Equals(object? obj) => obj switch
        {
            PackageIndex val => Equals(val),
            int val => Equals(val),
            _ => false
        };
        public bool Equals(int other) => other == _value;
        public bool Equals(PackageIndex other) => other.Equals(_value);

        public override int GetHashCode() => HashCode.Combine(_value);
        public static bool operator ==(PackageIndex left, PackageIndex right) => left.Equals(right);
        public static bool operator !=(PackageIndex left, PackageIndex right) => !(left == right);

        private int _value;
    }
}
