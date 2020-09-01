using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Structures;

namespace UETools.Objects.KismetVM.Instructions
{
    internal struct Transform : IUnrealSerializable
    {
        public FArchive Serialize(FArchive archive)
            => archive.Read(ref _rot)
                      .Read(ref _trans)
                      .Read(ref _scale);

        Vector4 _rot;
        Vector _trans;
        Vector _scale;
    }
}
