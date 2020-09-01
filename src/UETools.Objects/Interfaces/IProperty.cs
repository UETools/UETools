using System.IO;
using UETools.Core;

namespace UETools.Objects.Interfaces
{
    public interface IProperty
    {
        object Value { get; set; }

        FArchive Serialize(FArchive reader, PropertyTag tag);
    }
}
