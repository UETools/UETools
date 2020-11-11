using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Objects.Classes.Internal
{
    internal class StripDataFlags : IUnrealSerializable
    {
        public StripDataFlags() { }
        public StripDataFlags(FArchive archive) => Serialize(archive);

        public FArchive Serialize(FArchive archive) => archive.Read(ref _globalStripFlags).Read(ref _classStripFlags);

        private byte _classStripFlags;
        private byte _globalStripFlags;
    }
}
