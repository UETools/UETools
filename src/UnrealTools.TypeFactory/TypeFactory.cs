using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UnrealTools.TypeFactory
{
    public class TypeFactory<T>
    {
        public Dictionary<string, Func<T>> factories = new Dictionary<string, Func<T>>();
        internal TypeFactory(List<Type> types)
        {
            foreach (var type in types)
            {
                try
                {
                    factories.Add(type.Name, Factory.CreateInstanceFunction<T>(type));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }


        T GetInstance()
        {
            return GetFactory()();
        }

        Func<T> GetFactory() => factories.First().Value;
    }
}
