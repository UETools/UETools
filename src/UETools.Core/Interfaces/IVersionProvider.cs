using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core.Enums;

namespace UETools.Core.Interfaces
{
    public interface IVersionProvider
    {
        UE4Version Version { get; }
        int LicenseeVersion { get; }
        CustomVersionContainer CustomVersion { get; }
    }
}
