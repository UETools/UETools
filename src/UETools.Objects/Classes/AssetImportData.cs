using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Objects.Classes.Internal;

namespace UETools.Objects.Classes
{
    class AssetImportData : UObject
    {
        public override void Deserialize(FArchive reader)
        {
            reader.Read(out FString str);
            base.Deserialize(reader);
            return;
        }
    }
}
