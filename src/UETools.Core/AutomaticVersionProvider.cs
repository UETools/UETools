using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core.Enums;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    public class AutomaticVersionProvider : IVersionProvider
    {
        public readonly static AutomaticVersionProvider Instance = new AutomaticVersionProvider();
        public UE4Version Version => UE4Version.VER_UE4_AUTOMATIC_VERSION;
        public CustomVersionContainer CustomVersion { get; } = new CustomVersionContainer(CustomVersionSerializationFormat.Latest);
        public int LicenseeVersion => 0;
    }
}
