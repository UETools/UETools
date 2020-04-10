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
    class VorbisBankAsset : IUnrealDeserializable, IUnrealReadable
    {
        public void Deserialize(FArchive reader)
        {
        }

        public void ReadTo(IndentedTextWriter writer)
        {
        }
    }
}
