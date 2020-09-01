using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Asset
{
    internal class GatherableTextData : IUnrealSerializable
    {
        // TODO: Verify! GatherableTextData might not be implemented properly since I didn't have any samples using it.
        public FArchive Serialize(FArchive archive)
            => archive.Read(ref _namespaceName)
                      .Read(ref _sourceData)
                      .Read(ref _sourceSiteContexts);

        private FString _namespaceName = null!;
        private TextSourceData _sourceData = null!;
        private List<TextSourceSiteContext> _sourceSiteContexts = null!;
    }
}
