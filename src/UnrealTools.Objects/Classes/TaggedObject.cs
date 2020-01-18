using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using UnrealTools.Core;
using UnrealTools.Core.Interfaces;
using UnrealTools.Objects.Interfaces;
using UnrealTools.TypeFactory;
using TaggedItem = System.Collections.Generic.KeyValuePair<string, UnrealTools.Objects.Interfaces.IProperty>;
namespace UnrealTools.Objects.Classes
{
    using TaggedItemsList = List<TaggedItem>;
    public class TaggedObject : IUnrealDeserializable, IUnrealReadable
    {
        public TaggedObject() { }
        public TaggedObject(FArchive reader) => Deserialize(reader);
        public TaggedItemsList Vars { get; } = new TaggedItemsList();
        public IProperty this[string key] => Vars.Find(kv => kv.Key == key).Value;
        public virtual void Deserialize(FArchive reader)
        {
            foreach (var tag in PropertyTag.ReadToEnd(reader))
            {
                Vars.Add(new TaggedItem(tag.Name, PropertyFactory.Get(reader, tag)));
                if(reader.Tell() != tag.PropertyEnd)
                {
                    Console.WriteLine($"Needed to move by {tag.PropertyEnd - reader.Tell()} for {tag.Name} {(tag.TypeEnum == PropertyTag.PropertyType.StructProperty ? tag.StructName : tag.Type)}");
                    reader.Seek(tag.PropertyEnd);
                }
            }
        }

        public virtual void ReadTo(IndentedTextWriter writer)
        {
            writer.WriteLine('{');
            writer.Indent++;
            foreach (var kv in Vars)
            {
                writer.Write($"{kv.Key}: ");
                switch (kv.Value)
                {
                    case IUnrealReadable indent:
                        indent.ReadTo(writer);
                        break;
                    default:
                        writer.WriteLine(kv.Value);
                        break;
                }
            }
            writer.Indent--;
            writer.WriteLine('}');
        }

        internal static TaggedObject? Create(FArchive reader, string typename)
        {
            try
            {
                if (Classes.TryGetValue(typename, out var func))
                {
                    var x = func();
                    x.Deserialize(reader);
                    return x;
                }
                else
                {
                    var obj = new TaggedObject();
                    obj.Deserialize(reader);
                    return obj;
                }
            }
            catch
            {
                Console.WriteLine($"Class {typename}");
            }
            return null;
        }


        private static IReadOnlyDictionary<string, Func<TaggedObject>> Classes { get; } = new ReadOnlyDictionary<string, Func<TaggedObject>>(
            new TypeCollector<TaggedObject>(Assembly.GetCallingAssembly()).ToFactory().factories
            );
    }
}
