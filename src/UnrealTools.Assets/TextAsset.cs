using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Assets
{
    [UnrealAssetFile(".bin")]
    [UnrealAssetFile(".h")]
    [UnrealAssetFile(".txt")]
    [UnrealAssetFile(".ini")]
    [UnrealAssetFile(".json")]
    [UnrealAssetFile(".uplugin")]
    [UnrealAssetFile(".uproject")]
    [UnrealAssetFile(".upluginmanifest")]
    public sealed class TextAsset : IUnrealDeserializable, IUnrealReadable
    {
        public void Deserialize(FArchive reader) => reader.Read(out _content, (int)reader.Length());

        public void ReadTo(IndentedTextWriter writer)
        {
            writer.Indent = 0;
            writer.Write(MemoryMarshal.Cast<byte, char>(_content.Span));
        }

        private Memory<byte> _content;
    }
}
