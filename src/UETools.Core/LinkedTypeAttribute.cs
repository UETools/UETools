using System;

namespace UETools.Core
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class LinkedTypeAttribute : Attribute
    {
        public Type LinkedType { get; }

        public LinkedTypeAttribute(Type linkedType) => LinkedType = linkedType;
    }
}
