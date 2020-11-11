using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Objects.Classes
{
    public partial class Texture2D
    {
        public partial class TexturePlatformData
        {
            public class Texture2DMipMap : IUnrealSerializable
            {
                public byte[] Data => _data.Bytes;

                public FArchive Serialize(FArchive archive) => archive.Read(ref isCooked)
                                                                      .Read(ref _data)
                                                                      .Read(ref SizeX)
                                                                      .Read(ref SizeY)
                                                                      .Read(ref SizeZ);

                bool isCooked;
                ByteBulkData _data = null!;
                int SizeX;
                int SizeY;
                int SizeZ;

            }

        }
    }

}
