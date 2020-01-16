using System.IO;
using UnrealTools.Core;

namespace UnrealTools.Objects.Interfaces
{
    public interface IProperty
    {
        object Value { get; set; }

        void Deserialize(FArchive reader, PropertyTag tag);
    }
}
