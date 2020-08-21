using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using UETools.Assets.Internal.Asset;
using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Package;

namespace UETools.Assets
{
    [UnrealAssetFile(".uasset")]
    public sealed class UAssetAsset : IUnrealSerializable, IUnrealReadable
    {
        public bool IsValid => _summary.IsAssetFile;

        public FArchive Serialize(FArchive reader)
        {
            reader.Read(ref _summary);
            if (!IsValid)
                return reader;

            reader.Seek(_summary.NameOffset);
            _nameMap = new NameTable(_summary.NameCount);
            _nameMap.Serialize(reader);

            reader.Seek(_summary.ImportOffset);
            _imports = new ImportTable(_summary.ImportCount);
            _imports.Serialize(reader);

            reader.Seek(_summary.ExportOffset);
            _exports = new ExportTable(_summary.ExportCount);
            _exports.Serialize(reader);

            reader.Seek(_summary.SoftPackageReferencesOffset);
            reader.Read(ref _stringAssetReferences, _summary.SoftPackageReferencesCount);

            reader.Seek(_summary.GatherableTextDataOffset);
            reader.Read(ref _gatherableTextDataMap, _summary.GatherableTextDataCount);

            foreach (var imp in _imports.Items)
                imp.Fix(reader);
            foreach (var exp in _exports.Items)
                exp.Fix(reader);

            return reader;
        }

        public IEnumerable<ObjectExport> GetAssets() => _exports.GetAssets();

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
