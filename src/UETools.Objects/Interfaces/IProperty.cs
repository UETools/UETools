using System.IO;
using UETools.Core;

namespace UETools.Objects.Interfaces
{
    public interface IProperty
    {
        object Value { get; set; }

        void Deserialize(FArchive reader, PropertyTag tag);
    }
}
