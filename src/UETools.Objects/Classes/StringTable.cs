using System.CodeDom.Compiler;
using System.Collections.Generic;
using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Classes.Internal;

namespace UETools.Objects.Classes
{
    sealed class StringTable : UObject
    {
        class MetaDataMap : IUnrealSerializable
        {
            public Dictionary<FName, FString> Meta { get => _meta; set => _meta = value; }

            public FArchive Serialize(FArchive archive) => archive.Read(ref _meta);

            private Dictionary<FName, FString> _meta = null!;
        }

        public override FArchive Serialize(FArchive archive)
            => base.Serialize(archive)
                   .Read(ref _namespace)
                   .Read(ref _keySourceStringMap)
                   .Read(ref _keysToMetaData);

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
            foreach (var kv in _keySourceStringMap)
            {
                writer.WriteLine($"{kv.Key}: {kv.Value}");
            }
            writer.Indent--;
            writer.WriteLine($"END {_namespace}");
        }

        private FString _namespace = null!;
        private Dictionary<FString, FString> _keySourceStringMap = null!;
        private Dictionary<FString, MetaDataMap> _keysToMetaData = null!;
    }
}
