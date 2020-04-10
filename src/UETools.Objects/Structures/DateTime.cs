using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    struct DateTime : IUnrealStruct
    {
        public void Deserialize(FArchive reader) => _value = new System.DateTime(reader.Read(out long _));

        public override string ToString() => _value.ToString();

        System.DateTime _value;

    }
}
