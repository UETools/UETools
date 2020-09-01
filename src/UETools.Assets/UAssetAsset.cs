using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UETools.Assets.Internal.Asset;
using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Classes;
using UETools.Objects.Package;

namespace UETools.Assets
{
    [UnrealAssetFile(".uasset")]
    public sealed class UAssetAsset : IUnrealSerializable, IUnrealReadable
    {
        public bool IsValid => _summary.IsAssetFile;

        public IEnumerable<T> GetExportsOfType<T>() where T : TaggedObject => _exports.Items.Select(x => x.Object as T).OfType<T>();

        public FArchive Serialize(FArchive archive)
        {
            archive.Read(ref _summary);
            if (!IsValid)
                return archive;

            archive.Seek(_summary.NameOffset);
            _nameMap = new NameTable(_summary.NameCount);
            _nameMap.Serialize(archive);

            archive.Seek(_summary.ImportOffset);
            _imports = new ImportTable(_summary.ImportCount);
            _imports.Serialize(archive);

            archive.Seek(_summary.ExportOffset);
            _exports = new ExportTable(_summary.ExportCount);
            _exports.Serialize(archive);

            archive.Seek(_summary.SoftPackageReferencesOffset);
            archive.Read(ref _stringAssetReferences, _summary.SoftPackageReferencesCount);

            archive.Seek(_summary.GatherableTextDataOffset);
            archive.Read(ref _gatherableTextDataMap, _summary.GatherableTextDataCount);

            foreach (var imp in _imports.Items)
                imp.Fix(archive);
            foreach (var exp in _exports.Items)
                exp.Fix(archive);

            return archive;
        }

        private IEnumerable<ObjectExport> GetAssets() => _exports.GetAssets();

        public ObjectExport? GetClass() => _exports.GetClass();

        public void ReadTo(IndentedTextWriter writer)
        {
            var main = _exports.GetClass();
            if (main is null)
            {
                var assets = _exports.GetAssets();
                foreach(var asset in assets)
                {
                    writer.WriteLine($"Asset: {asset.FullName}");
                    asset.Object?.ReadTo(writer);
                }
            }
            else
                main.Object?.ReadTo(writer);

            var cdo = _exports.GetCDO();
            if (cdo is null)
                return;
            writer.WriteLine("<--- CDO --->");
            cdo.Object?.ReadTo(writer);
            return;
        }

        PackageFileSummary _summary = null!;
        NameTable _nameMap = null!;
        ImportTable _imports = null!;
        ExportTable _exports = null!;
        List<FString> _stringAssetReferences = null!;
        List<GatherableTextData> _gatherableTextDataMap = null!;
    }
}
