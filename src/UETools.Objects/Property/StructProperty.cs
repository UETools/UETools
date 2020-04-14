using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                var obj = new TaggedObject();
                try
                {
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