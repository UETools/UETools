using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;
using UnrealTools.Pak.Interfaces;

namespace UnrealTools.Pak
{
    public sealed class PakVFS : IDisposable, IAsyncDisposable
    {
        public Dictionary<string, PakEntry> AbsoluteIndex { get; }
        public PakFileIndex<PakEntry> Index 
        { 
            get
            {
                if(_index is null)
                    _index = PakFileIndex<PakEntry>.Parse(AbsoluteIndex);

                return _index;
            }
        }

        private PakVFS(IEnumerable<PakFile> files)
        {
            _pakFiles = new List<PakFile>(files);
            AbsoluteIndex = new Dictionary<string, PakEntry>(_pakFiles.SelectMany(f => f.AbsoluteIndex));
        }

        public static PakVFS OpenAt(string path, IVersionProvider? versionProvider = null, IAesKeyProvider? keyProvider = null)
        {
            if (path is null) 
                throw new ArgumentNullException(nameof(path));
            var dir = new DirectoryInfo(path);
            if (!dir.Exists)
                throw new DirectoryNotFoundException();

            var paks = dir.GetFiles(PakExtensionPattern).Select(f => PakFile.Open(f, versionProvider, keyProvider)).OfType<PakFile>();
            return new PakVFS(paks);
        }
        public async static ValueTask<PakVFS> OpenAtAsync(string path, IVersionProvider? versionProvider = null, IAesKeyProvider? keyProvider = null, CancellationToken cancellationToken = default)
        {
            if (path is null) throw new ArgumentNullException(nameof(path));
            var dir = new DirectoryInfo(path);
            if (!dir.Exists)
                throw new DirectoryNotFoundException();

            var files = dir.GetFiles(PakExtensionPattern);
            var tasks = files.Select(f => PakFile.OpenAsync(f, versionProvider, cancellationToken: cancellationToken));
            var paks = await Task.WhenAll(tasks).ConfigureAwait(false);
            return new PakVFS(paks.Where(x => x != null));
        }

        public void Dispose()
        {
            foreach (var pak in _pakFiles)
                pak.Dispose();
        }
        public async ValueTask DisposeAsync()
        {
            foreach (var pak in _pakFiles)
                await pak.DisposeAsync().ConfigureAwait(false);
        }

        private const string PakExtensionPattern = "*.pak";

        private readonly List<PakFile> _pakFiles;
        private PakFileIndex<PakEntry>? _index;
    }
}
