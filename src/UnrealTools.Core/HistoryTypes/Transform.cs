using System;
using UnrealTools.Core.Enums;

namespace UnrealTools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class Transform : FTextHistory
        {
            public override void Deserialize(FArchive reader)
            {
                base.Deserialize(reader);

                reader.Read(out _text);
                reader.ReadUnsafe(out _transform);
            }

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
