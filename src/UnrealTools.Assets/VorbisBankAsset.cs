using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Assets
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
