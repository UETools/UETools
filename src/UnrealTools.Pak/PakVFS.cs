using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Pak
{
    public sealed class PakVFS : IDisposable, IAsyncDisposable
    {
        public Dictionary<string, PakEntry> AbsoluteIndex { get; }

        private PakVFS(IEnumerable<PakFile> files)
        {
            _pakFiles = new List<PakFile>(files);
            AbsoluteIndex = new Dictionary<string, PakEntry>(_pakFiles.SelectMany(f => f.AbsoluteIndex));
        }

        public static PakVFS OpenAt(string path) => OpenAt(path, AutomaticVersionProvider.Instance);
        public static PakVFS OpenAt(string path, IVersionProvider versionProvider)
        {
            if (path is null) throw new ArgumentNullException(nameof(path));
            if (versionProvider is null) throw new ArgumentNullException(nameof(path));

            var dir = new DirectoryInfo(path);
            if (!dir.Exists)
                throw new DirectoryNotFoundException();

            var provider = versionProvider;
            var paks = dir.GetFiles(PakExtensionPattern).Select(f => PakFile.Open(f, provider)).OfType<PakFile>();
            return new PakVFS(paks);
        }
        public async static ValueTask<PakVFS?> OpenAtAsync(string path)
        {
            if (path is null) throw new ArgumentNullException(nameof(path));

            var dir = new DirectoryInfo(path);
            if (!dir.Exists)
                throw new DirectoryNotFoundException();

            var files = dir.GetFiles(PakExtensionPattern);

            if (files.Length > 0)
            {
                var tasks = files.Select(f => PakFile.OpenAsync(f));
                var paks = await Task.WhenAll(tasks).ConfigureAwait(false);
                return new PakVFS(paks.Where(x => x != null));
            }
            return null;
        }

        public void Dispose()
        {
            foreach (var pak in _pakFiles)
                pak.Dispose();
        }
        public async ValueTask DisposeAsync()
        {
            foreach (var pak in _pakFiles)
                await pak.DisposeAsync();
        }

        private const string PakExtensionPattern = "*.pak";

        private readonly List<PakFile> _pakFiles;
    }
}
