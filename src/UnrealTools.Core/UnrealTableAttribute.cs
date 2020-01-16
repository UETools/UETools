using System;

namespace UnrealTools.Core
{
    /// <summary>
    /// Determines the name that instance of a class will be added as in the table dictionary.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class UnrealTableAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name associated with instances of the class.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Initializes attribute with the specified name.
        /// </summary>
        /// <param name="name">Name to associate with the instance of this class.</param>
        public UnrealTableAttribute(string name) => Name = name;
    }
}
