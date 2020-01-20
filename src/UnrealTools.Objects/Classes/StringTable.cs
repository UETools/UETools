using System.CodeDom.Compiler;
using System.Collections.Generic;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Objects.Classes
{
    sealed class StringTable : UObject
    {
        class MetaDataMap : IUnrealDeserializable
        {
            public Dictionary<FName, FString> Meta { get => _meta; set => _meta = value; }

            public void Deserialize(FArchive reader) => reader.Read(out _meta);

            private Dictionary<FName, FString> _meta = null!;
        }

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.Read(out _namespace);
            reader.Read(out _keySourceStringMap);
            reader.Read(out _keysToMetaData);
        }

        public override void ReadTo(IndentedTextWriter writer)
        {
            if (_namespace is null || _keySourceStringMap is null || _keysToMetaData is null)
                NotDeserializedException.Throw();

            base.ReadTo(writer);
            writer.WriteLine();
            writer.WriteLine($"{nameof(StringTable)} content");
            writer.WriteLine();


            writer.WriteLine($"BEGIN {_namespace}");
            writer.Indent++;
            foreach (var (k, v) in _keySourceStringMap)
            {
                writer.WriteLine($"{k}: {v}");
            }
            writer.Indent--;
            writer.WriteLine($"END {_namespace}");
        }

        private FString _namespace = null!;
        private Dictionary<FString, FString> _keySourceStringMap = null!;
        private Dictionary<FString, MetaDataMap> _keysToMetaData = null!;
    }
}
