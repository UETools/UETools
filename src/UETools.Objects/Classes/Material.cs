using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Objects.Classes.Internal;

namespace UETools.Objects.Classes
{
    sealed class Material : UObject
    {
        public override FArchive Serialize(FArchive reader)
        {
            base.Serialize(reader);
            // TODO: Material data
            return reader;
        }
    }
}
