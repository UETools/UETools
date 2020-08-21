using System.Diagnostics;
using UETools.Core;
using UETools.Objects.Enums;
using UETools.Objects.Interfaces;
using UETools.Objects.Package;

namespace UETools.Objects.Structures
{
    public struct FontData : IUnrealStruct
    {
        public FArchive Serialize(FArchive reader)
        {
            reader.Read(ref _bIsCooked);
            if (_bIsCooked)
            {
                reader.Read(ref _fontFaceAsset);

                if(_fontFaceAsset.Resource is null)
                {
                    reader.Read(ref _fontFileName);
                    reader.ReadUnsafe(ref _hinting);
                    reader.ReadUnsafe(ref _loadingPolicy);
                }

                reader.Read(ref _subFaceIndex);
            }
            else
                Debugger.Break();

            return reader;
        }

        private bool _bIsCooked;
        private FString _fontFileName;
        private EFontHinting _hinting;
        private EFontLoadingPolicy _loadingPolicy;
        private ObjectReference _fontFaceAsset;
        private int _subFaceIndex;
    }
}
