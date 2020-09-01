using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets
{
    // TODO: Implement
    [UnrealAssetFile(".bnk")]
    class VorbisBankAsset : IUnrealSerializable, IUnrealReadable
    {
        public FArchive Serialize(FArchive archive) => archive;

        public void ReadTo(IndentedTextWriter writer)
        {
        }
    }
}
