using System.Diagnostics;
using UETools.Core;
using UETools.Objects.Enums;
using UETools.Objects.Interfaces;
using UETools.Objects.Package;

namespace UETools.Objects.Structures
{
    public struct FontData : IUnrealStruct
    {
        public FArchive Serialize(FArchive archive)
        {
            archive.Read(ref _bIsCooked);
            if (_bIsCooked)
            {
                archive.Read(ref _fontFaceAsset);

                if(_fontFaceAsset.Resource is null)
                {
                    archive.Read(ref _fontFileName)
                           .ReadUnsafe(ref _hinting)
                           .ReadUnsafe(ref _loadingPolicy);
                }

                archive.Read(ref _subFaceIndex);
            }

            Debug.WriteLineIf(!_bIsCooked, "FontData is not cooked.");

            return archive;
        }

        private bool _bIsCooked;
        private FString _fontFileName;
        private EFontHinting _hinting;
        private EFontLoadingPolicy _loadingPolicy;
        private ObjectReference _fontFaceAsset;
        private int _subFaceIndex;
    }
}
