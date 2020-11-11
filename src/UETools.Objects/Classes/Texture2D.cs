using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Classes.Internal;

namespace UETools.Objects.Classes
{
    public partial class Texture2D : Texture
    {
        public TexturePlatformData PlatformData => _platformData;

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);

            var dataFlags = new StripDataFlags(archive);

            archive.Read(ref _isCooked);
            if (_isCooked is not 0)
            {
                FName? _pixelFormatEnum = null;
                archive.Read(ref _pixelFormatEnum);
                while (!_pixelFormatEnum.IsNone())
                {
                    long skipOffset = 0;
                    archive.Read(ref skipOffset);
                    archive.Read(ref _platformData);
                    archive.Read(ref _pixelFormatEnum);
                    continue;
                }
            }

            return archive;
        }

        private int _isCooked;
        private TexturePlatformData _platformData = null!;
    }

}
