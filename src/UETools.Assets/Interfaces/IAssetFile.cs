using System.Text.RegularExpressions;
using UETools.Core;

namespace UETools.Assets.Interfaces
{
    internal interface IAssetFile
    {
        public Regex Filename { get; }

        bool IsValidMagic(FArchive reader);
    }
}