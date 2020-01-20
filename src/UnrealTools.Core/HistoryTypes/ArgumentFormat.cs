using System.Collections.Generic;

namespace UnrealTools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class ArgumentFormat : FTextHistory
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

                var EndValue = _format.ToString();
                foreach (var arg in _arguments)
                    EndValue = arg.Replace(EndValue);

                return EndValue;
            }

            private FText _format = null!;
            private List<FFormatArgumentData> _arguments = null!;
        }
    }
}
