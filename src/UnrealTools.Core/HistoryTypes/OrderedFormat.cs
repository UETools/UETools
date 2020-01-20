using System.Collections.Generic;

namespace UnrealTools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class OrderedFormat : FTextHistory
        {
            public override void Deserialize(FArchive reader)
            {
                base.Deserialize(reader);

                reader.Read(out _format);
                reader.Read(out _arguments);
            }

            public override string ToString()
            {
                if (_format is null || _arguments is null)
                    NotDeserializedException.Throw();

                return string.Format(_format.ToString(), _arguments.ToArray());
            }

            private FText _format = null!;
            private List<FFormatArgumentValue> _arguments = null!;
        }
    }
}
