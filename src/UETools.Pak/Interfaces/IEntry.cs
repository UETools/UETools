using System.Threading;
using System.Threading.Tasks;
using UETools.Core;

namespace UETools.Pak.Interfaces
{
    public interface IEntry
    {
        FArchive Read();
        ValueTask<FArchive> ReadAsync(CancellationToken cancellationToken = default);
    }
}
