using System.Diagnostics;
using UETools.Core;
using UETools.Objects.Enums;
using UETools.Objects.Interfaces;
using UETools.Objects.Package;

namespace UETools.Objects.Structures
{
    public struct FontData : IUnrealStruct
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _bIsCooked);
            if (_bIsCooked)
            {
                reader.Read(out _fontFaceAsset);

                if(_fontFaceAsset.Resource is null)
                {
                    reader.Read(out _fontFileName);
                    reader.ReadUnsafe(out _hinting);
                    reader.ReadUnsafe(out _loadingPolicy);
                }

                reader.Read(out _subFaceIndex);
            }
            else
                Debugger.Break();
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        private bool _bIsCooked;
        private FString _fontFileName;
        private EFontHinting _hinting;
        private EFontLoadingPolicy _loadingPolicy;
        private ObjectReference _fontFaceAsset;
        private int _subFaceIndex;
    }
}
