﻿using System.CodeDom.Compiler;
using System.Diagnostics;
using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Property
{
    [DebuggerDisplay("{_value}", Type = "Property")]
    abstract class UProperty<T> : IProperty, IUnrealReadable where T : notnull
    {
        object IProperty.Value { get => _value; set => _value = (T)value; }

        public abstract void Deserialize(FArchive reader, PropertyTag tag);

        public virtual void ReadTo(IndentedTextWriter writer) => ReadObject(writer, _value);

        protected static void ReadObject<TValue>(IndentedTextWriter writer, TValue obj)
        {
            switch (obj)
            {
                case IUnrealReadable indent:
                    indent.ReadTo(writer);
                    break;
                default:
                    writer.WriteLine(obj);
                    break;
            }
        }

        protected T _value = default!;
    }
}
