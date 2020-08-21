using System;
using UETools.Core.Enums;

namespace UETools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class Transform : FTextHistory
        {
            public override FArchive Serialize(FArchive reader) 
                => reader.Read(ref _text)
                         .ReadUnsafe(ref _transform);

            public override string ToString()
            {
                if (_text is null)
                    NotDeserializedException.Throw();

                return _transform switch
                {
                    TransformType.ToLower => _text.ToString().ToLowerInvariant(),
                    TransformType.ToUpper => _text.ToString().ToUpperInvariant(),
                    _ => throw new NotImplementedException($"TransformType {_transform} not implemented")
                };
            }

            private TransformType _transform;
            private FText _text = null!;
        }
    }
}
