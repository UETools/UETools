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
        private static IReadOnlyDictionary<string, Func<IUnrealStruct>> Structures { get; } = new ReadOnlyDictionary<string, Func<IUnrealStruct>>(new TypeCollector<IUnrealStruct>(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!).ToFactory().factories);

        private string? _unsuccessfulStruct;
        public override void Deserialize(FArchive reader, PropertyTag tag)
        {
            var structType = tag.StructName?.ToString();
            if (structType != null && Structures.TryGetValue(structType, out var factory))
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
                    var obj = new TaggedObject();
                    _value = obj;
                    obj.Deserialize(reader);
                    
                }
                catch
                {
                    Console.WriteLine(structType);
                    _unsuccessfulStruct = $"{{ {structType} needs native deserialization }}";
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