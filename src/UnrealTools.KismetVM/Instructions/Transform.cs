using UnrealTools.Core;
using UnrealTools.Core.Interfaces;
using UnrealTools.Objects.Structures;

namespace UnrealTools.KismetVM.Instructions
{
    internal struct Transform : IUnrealDeserializable
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _rot);
            reader.Read(out _trans);
            reader.Read(out _scale);
        }

        Vector4 _rot;
        Vector _trans;
        Vector _scale;
    }
}
