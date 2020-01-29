using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UnrealTools.TypeFactory
{
    public class TypeCollector<T>
    {
        private const string _searchPattern = "*.dll";
        public TypeCollector(Assembly fromAssembly) : this(fromAssembly, false) { }
        public TypeCollector(Assembly fromAssembly, bool collectGenerics) => GetAssemblyTypes(fromAssembly, collectGenerics);
        public TypeCollector(DirectoryInfo fromDirectory) : this(fromDirectory, false) { }
        public TypeCollector(DirectoryInfo fromDirectory, bool collectGenerics)
        {
            var files = fromDirectory.GetFiles(_searchPattern);
            foreach (var file in files)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(file.FullName);
                    GetAssemblyTypes(assembly, collectGenerics);
                }
                catch (Exception)
                {

                }
            }
        }

        public TypeCollector(string path) : this(path, false) { }
        public TypeCollector(string path, bool collectGenerics) : this(new DirectoryInfo(path), collectGenerics) { }

        public TypeFactory<T> ToFactory()
        {
            try
            {
                return new TypeFactory<T>(typesCollection);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        private void GetAssemblyTypes(Assembly assembly, bool collectGenerics) => typesCollection.AddRange(assembly.GetTypes()
            .Where(t =>
                typeof(T).IsAssignableFrom(t)
                && !t.IsAbstract
                && !t.IsInterface
                // && t.IsVisible
                && (collectGenerics || !t.IsGenericType)
            ));

        public List<Type> typesCollection = new List<Type>();
    }
}
