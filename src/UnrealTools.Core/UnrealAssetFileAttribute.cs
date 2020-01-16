using System;
using System.IO;

namespace UnrealTools.Core
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = true)]
    public class UnrealAssetFileAttribute : Attribute
    {
        public string Extension { get; }
        public string FileName { get; }
        public bool HasWildcard { get; }

        public UnrealAssetFileAttribute(string extension) : this("*", extension, true) { }
        public UnrealAssetFileAttribute(string fileName, bool hasWildcard) : this(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName), hasWildcard) { }
        private UnrealAssetFileAttribute(string fileName, string extension, bool hasWildcard)
        {
            FileName = fileName;
            Extension = extension;
            HasWildcard = hasWildcard;
        }
    }
}
