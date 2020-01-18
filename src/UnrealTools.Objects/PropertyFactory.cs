using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;
using UnrealTools.TypeFactory;

namespace UnrealTools.Objects
{
    public static class PropertyFactory
    {
        private static TypeCollector<IProperty> TypeCollector = new TypeCollector<IProperty>(Assembly.GetCallingAssembly(), true);
        private static TypeFactory<IProperty> TypeFactory = TypeCollector.ToFactory();
        private static bool HasType(string value, [NotNullWhen(true)] out Type? type)
        {
            type = TypeCollector.typesCollection.Find(x => x.Name == value);
            return type != null;
        }

        public static IProperty Get(FArchive reader, PropertyTag tag)
        {
            if (HasType(tag.Type.Name.Value, out var type))
            {
                var factory = TypeFactory.factories;
                if (factory.TryGetValue(tag.Type.ToString(), out var func))
                {
                    var it = func();
                    it.Deserialize(reader, tag);
                    return it;
                }
                else if(type.IsGenericType)
                {
                    if (!type.IsConstructedGenericType && HasType(tag.InnerType.ToString(), out var inner))
                    {
                        type = type.MakeGenericType(inner);
                    }
                    var fun = Factory.CreateInstanceFunction<IProperty>(type);
                    var it = fun();
                    it.Deserialize(reader, tag);
                    return it;
                }
            }
            return null;
        }

        internal static Func<IProperty> Get(Type type)
        {
            if (TypeFactory.factories.TryGetValue(type.Name, out var func))
                return func;
            else
                return Factory.CreateInstanceFunction<IProperty>(type);
        }
    }
}
