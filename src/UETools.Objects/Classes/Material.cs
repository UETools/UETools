using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core;

namespace UETools.Objects.Classes
{
    sealed class Material : UObject
    {
        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            // TODO: Material data
            return;
        }
    }
}
