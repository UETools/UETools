using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets
{
    [UnrealAssetFile(".bin")]
    [UnrealAssetFile(".h")]
    [UnrealAssetFile(".txt")]
    [UnrealAssetFile(".ini")]
    [UnrealAssetFile(".json")]
    [UnrealAssetFile(".uplugin")]
    [UnrealAssetFile(".uproject")]
    [UnrealAssetFile(".upluginmanifest")]
    public sealed class TextAsset : IUnrealSerializable, IUnrealReadable
    {
        public FArchive Serialize(FArchive archive) => archive.Read(ref _content, (int)archive.Length());

        public void ReadTo(IndentedTextWriter writer)
        {
            throw new NotImplementedException();
        }

        private Memory<byte> _content;
    }
}
