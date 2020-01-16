using System.Threading;
using System.Threading.Tasks;
using UnrealTools.Core;

namespace UnrealTools.Pak.Interfaces
{
    public interface IEntry
    {
        FArchive Read();
        ValueTask<FArchive> ReadAsync(CancellationToken cancellationToken = default);
    }
}
