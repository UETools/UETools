using System.Collections.Generic;
using UETools.Core;
using UETools.Objects.Classes.Internal;

namespace UETools.Objects.Classes
{
    public class Texture : UObject
    {
        public override FArchive Serialize(FArchive archive) => base.Serialize(archive).Read(ref _stripDataFlags);

        private StripDataFlags? _stripDataFlags;
    }
}