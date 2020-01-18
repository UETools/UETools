using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;
using UnrealTools.Objects.Classes;
using UnrealTools.Objects.Interfaces;
using UnrealTools.Objects.Structures;
using UnrealTools.TypeFactory;

namespace UnrealTools.Objects.Property
{
    internal sealed class StructProperty : UProperty<object>
    {
        private static IReadOnlyDictionary<string, Func<IUnrealStruct>> Structures { get; } = new ReadOnlyDictionary<string, Func<IUnrealStruct>>(new TypeCollector<IUnrealStruct>(Assembly.GetCallingAssembly()).ToFactory().factories);

        private string? _unsuccessfulStruct;
        public override void Deserialize(FArchive reader, PropertyTag tag)
        {
            var structType = tag.StructName?.ToString();
            if (structType == "SmartName")
            {
                IUnrealStruct val = tag.Size switch
                {
                    8 => new SmartNameShort(),
                    10 => new SmartName(),
                    26 => new SmartNameGuid()
                };
                val.Deserialize(reader);
                _value = val;
                
            }
            else if (structType != null && Structures.TryGetValue(structType, out var factory))
            {
                var val = factory();
                val.Deserialize(reader);
                _value = val;
            }
            else if (tag.Size <= 8)
                _unsuccessfulStruct = $"{structType}: {BitConverter.ToString(reader.Read(out byte[] _, tag.Size))}";
            else
            {
                try
                {
                    _value = new TaggedObject(reader);
                }
                catch
                {
                    Console.WriteLine(structType);
                    _unsuccessfulStruct = $"{{ {structType} needs native deserialization }}";
                    reader.Seek(tag.PropertyEnd);
                }
            }
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