﻿using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    struct DateTime : IUnrealStruct
    {
        public FArchive Serialize(FArchive archive)
        {
            long ticks = _value.Ticks;
            archive.Read(ref ticks);
            _value = new System.DateTime(ticks);
            return archive;
        }

        public override string ToString() => _value.ToString();

        System.DateTime _value;

    }
}
