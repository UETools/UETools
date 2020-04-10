using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using UETools.Core;
using UETools.Objects.Interfaces;
using UETools.TypeFactory;

namespace UETools.Objects
{
    public static class PropertyFactory
    {
        private static TypeCollector<IProperty> TypeCollector = new TypeCollector<IProperty>(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!, true);
        private static TypeFactory<IProperty> TypeFactory = TypeCollector.ToFactory();
        
        public static IProperty Get(FArchive reader, PropertyTag tag)
        {
            var factory = TypeFactory.factories;
            var it = factory[tag.Type.ToString()]();
            it.Deserialize(reader, tag);
            return it;
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
