using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Classes;
using UETools.Objects.Interfaces;
using UETools.Objects.Structures;
using UETools.TypeFactory;

namespace UETools.Objects.Property
{
    internal sealed class StructProperty : UProperty<object>
    {
        private static IReadOnlyDictionary<string, Func<IUnrealStruct>> Structures { get; } = new ReadOnlyDictionary<string, Func<IUnrealStruct>>(new TypeCollector<IUnrealStruct>(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!).ToFactory().factories);

        private string? _unsuccessfulStruct;
        public override FArchive Serialize(FArchive reader, PropertyTag tag)
        {
            var structType = tag.StructName?.ToString();
            if (structType != null && Structures.TryGetValue(structType, out var factory))
            {
                var val = factory();
                val.Serialize(reader);
                _value = val;
            }
            else if (tag.Size <= 8)
            {
                byte[]? bytes = default;
                reader.Read(ref bytes, tag.Size);
                _unsuccessfulStruct = $"{structType}: {BitConverter.ToString(bytes)}";
            }
            else
            {
                var obj = new TaggedObject();
                try
                {
                    _value = obj;
                    obj.Serialize(reader);

                }
                catch
                {
                    _unsuccessfulStruct = $"{{ {structType} needs native deserialization }}";
                    Debug.WriteLine(_unsuccessfulStruct);
                }
            }
            return reader;
        }

        public override void ReadTo(IndentedTextWriter writer)
        {
            if(_unsuccessfulStruct != null)
            {
                writer.WriteLine(_unsuccessfulStruct);
            }
            else
            {
                switch(_value)
                {
                    case IUnrealReadable indent:
                        indent.ReadTo(writer);
                        break;
                    default:
                        writer.WriteLine(_value);
                        break;
                }
            }
        }
    }
}