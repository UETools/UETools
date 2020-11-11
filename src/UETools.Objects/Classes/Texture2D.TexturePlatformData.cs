using System;
using System.Collections.Generic;
using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Enums;

namespace UETools.Objects.Classes
{
    public partial class Texture2D
    {
        public partial class TexturePlatformData : IUnrealSerializable
        {
            public int SizeX => _sizeX;
            public int SizeY => _sizeY;
            public Texture2DMipMap FirstMip => Mips[_firstMip];
            public List<Texture2DMipMap> Mips => _mips;
            public EPixelFormat PixelFormat { get; private set; }
            
            public FArchive Serialize(FArchive archive)
            {
                archive.Read(ref _sizeX)
                       .Read(ref _sizeY)
                       .Read(ref _numSlices)
                       .Read(ref _pixelFormat)
                       .Read(ref _firstMip)
                       .Read(ref _mips)
                       .Read(ref _isVirtual);

                if(Enum.TryParse<EPixelFormat>(_pixelFormat.ToString(), out var format))
                {
                    PixelFormat = format;
                }

                return archive;
            }

            int _sizeX;
            int _sizeY;
            int _numSlices;
            int _firstMip;
            FString _pixelFormat;
            List<Texture2DMipMap> _mips;
            bool _isVirtual;
        }
    }
}
