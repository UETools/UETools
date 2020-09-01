using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using UETools.Core;
using UETools.Core.Interfaces;
using UETools.Objects.Interfaces;
using UETools.TypeFactory;
using TaggedItem = System.Collections.Generic.KeyValuePair<string, UETools.Objects.Interfaces.IProperty>;
namespace UETools.Objects.Classes
{
    using TaggedItemsList = List<TaggedItem>;
    public class TaggedObject : IUnrealSerializable, IUnrealReadable
    {
        public TaggedObject() { }
        public TaggedObject(FArchive reader) => Serialize(reader);
        public TaggedItemsList Vars { get; } = new TaggedItemsList();
        public IProperty this[string key] => Vars.Find(kv => kv.Key == key).Value;
        public virtual FArchive Serialize(FArchive archive)
        {
            foreach (var tag in PropertyTag.ReadToEnd(archive))
            {
                Vars.Add(new TaggedItem(tag.Name, PropertyFactory.Get(archive.SubStream(tag.Size), tag)));
                if(archive.Tell() != tag.PropertyEnd)
                {
                    Debug.WriteLine($"Needed to move by {tag.PropertyEnd - archive.Tell()} for {tag.Name} {(tag.TypeEnum == PropertyTag.PropertyType.StructProperty ? tag.StructName : tag.Type)}");
                    archive.Seek(tag.PropertyEnd);
                }
            }
            return archive;
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
                    x.Serialize(reader);
                    return x;
                }
                else
                {
                    var obj = new TaggedObject();
                    obj.Serialize(reader);
                    return obj;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Class {typename}");
            }
            return null;
        }


        private static IReadOnlyDictionary<string, Func<TaggedObject>> Classes { get; } = new ReadOnlyDictionary<string, Func<TaggedObject>>(
            new TypeCollector<TaggedObject>(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!).ToFactory().factories
            );
    }
}
