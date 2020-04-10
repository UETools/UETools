using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Assets.Internal.Asset
{
    internal class GatherableTextData : IUnrealDeserializable
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _namespaceName);
            reader.Read(out _sourceData);
            reader.Read(out _sourceSiteContexts);
            throw new UnrealException($"{nameof(GatherableTextData)} is not implemented properly since I didn't have any samples using it. Feel free to share it with me!", new NotImplementedException());
        }

        private FString _namespaceName = null!;
        private TextSourceData _sourceData = null!;
        private List<TextSourceSiteContext> _sourceSiteContexts = null!;
    }
}
