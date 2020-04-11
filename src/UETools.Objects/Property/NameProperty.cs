﻿using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Property
{
    internal sealed class NameProperty : UProperty<FName>
    {
        public override void Deserialize(FArchive reader, PropertyTag tag) => reader.Read(out _value);
    }
}