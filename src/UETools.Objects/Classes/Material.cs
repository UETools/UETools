using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Objects.Classes.Internal;

namespace UETools.Objects.Classes
{
    sealed class Material : UObject
    {
        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);
            // TODO: Material data
            return archive;
        }
    }
}
