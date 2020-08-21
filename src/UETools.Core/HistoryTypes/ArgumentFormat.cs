using System.Collections.Generic;

namespace UETools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class ArgumentFormat : FTextHistory
        {
            public override FArchive Serialize(FArchive reader) 
                => reader.Read(ref _format)
                         .Read(ref _arguments);

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
