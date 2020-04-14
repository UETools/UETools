using System;
using System.Diagnostics;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    [DebuggerDisplay("{Name.ToString()}")]
    public class FName : IUnrealDeserializable, IEquatable<FName>, IEquatable<FString>, IEquatable<string>
    {
        /// <summary>
        /// Invalid index, signifies uninitialized FName.
        /// </summary>
        public const int INDEX_NONE = -1;

        public static implicit operator string(FName name) => name.Name.ToString();
        public static implicit operator FName(string str) => new FName(str);
        public static implicit operator FName(FString str) => new FName(str);
        public static bool operator ==(FName first, FName second) => first.Equals(second);
        public static bool operator !=(FName first, FName second) => !first.Equals(second);

        public FString Name { get; private set; }
        private int Index => _displayId;

        public FName(FString name) => Name = name;
        public FName(string name) : this(new FString(name)) { }
        public FName() : this(new FString()) { }

        public void Deserialize(FArchive reader)
        {
            reader.Read(out _displayId);
            reader.Read(out _comparisionId);

            if (reader.GetTable<FString>("Names") is NameTable table)
            {
                var items = table.Items;
                if (items.Count > Index && Index >= 0)
                    Name = items[Index];
                else
                    throw new ArgumentOutOfRangeException(nameof(Index), $"{Index} not in GNames({items.Count} elements).");
            }
            else
                throw new UnrealException($"{nameof(NameTable)} not serialized before {nameof(FName)} access.");
        }
        public bool IsNone() => IsIndexNone() || IsStringNone();
        private bool IsIndexNone() => Index == INDEX_NONE;
        private bool IsStringNone() => Name == string.Empty || Name == "None";

        public override int GetHashCode() => HashCode.Combine(Name, Index);
        public bool Equals(FName other) => IsIndexNone() || other.IsIndexNone() ? other.Equals(Name) : Index == other.Index && _comparisionId == other._comparisionId;
        public bool Equals(FString other) => other.Equals(Name);
        public bool Equals(string other) => other == Name;
        public override bool Equals(object? obj) => obj switch
        {
            FName other => Equals(other),
            FString other => Equals(other),
            string other => Equals(other),
            _ => base.Equals(obj)
        };

        public override string ToString() => Name.ToString();


        private int _displayId = INDEX_NONE;
        private int _comparisionId;
    }
}
