﻿using System.Collections.Generic;

namespace UETools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class OrderedFormat : FTextHistory
        {
            public override FArchive Serialize(FArchive reader) 
                => reader.Read(ref _format)
                         .Read(ref _arguments);

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
