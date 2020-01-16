using System.Text.RegularExpressions;
using UnrealTools.Core;

namespace UnrealTools.Assets.Interfaces
{
    internal interface IAssetFile
    {
        public Regex Filename { get; }

        bool IsValidMagic(FArchive reader);
    }
}