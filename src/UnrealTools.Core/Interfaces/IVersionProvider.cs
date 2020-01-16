using System;
using System.Collections.Generic;
using System.Text;
using UnrealTools.Core.Enums;

namespace UnrealTools.Core.Interfaces
{
    public interface IVersionProvider
    {
        UE4Version Version { get; }
        int LicenseeVersion { get; }
        CustomVersionContainer CustomVersion { get; }
    }
}
