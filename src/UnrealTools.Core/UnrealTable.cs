using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnrealTools.Core.Interfaces;
using UnrealTools.Core.Interfaces.Generic;

namespace UnrealTools.Core
{
    /// <summary>
    /// Base class of every table type that's supposed to be added to internal FArchive storage.
    /// </summary>
    /// <remarks>
    /// Deriving class is expected to be sealed and define UnrealTableAttribute, otherwise constructor throws NotImplementedException.
    /// </remarks>
    /// <typeparam name="T">Type of items stored in the table.</typeparam>
    [DebuggerDisplay("{Items.Count} items")]
    public abstract class UnrealTable<T> : IUnrealDeserializable, IUnrealTable<T> where T : notnull
    {
        /// <summary>
        /// <see cref="List{T}"/> of elements exposed by the table.
        /// </summary>
        public List<T> Items => _entries;
        /// <summary>
        /// Support for predefined amount of entries in the table. Should be used if the array doesn't have its count serialized before other elements.
        /// </summary>
        protected int? ItemCount { get; set; }

        /// <summary>
        /// Checks if class implements UnrealTableAttribute and is sealed.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// Thrown when class doesn't define <see cref="UnrealTableAttribute"/>, or isn't marked <see langword="sealed"/>.
        /// </exception>
        protected UnrealTable()
        {
            var t = GetType();
            if (!t.IsDefined(typeof(UnrealTableAttribute), false) && t.IsSealed)
                ThrowNotImplemented(t.Name);
        }

        /// <summary>
        /// Sets predefined length of the list.
        /// </summary>
        /// <param name="length">Value to assign to <see cref="ItemCount"/></param>
        public UnrealTable(int length) : this() => ItemCount = length;

        /// <summary>
        /// Adds class instance to <see cref="FArchive.Tables"/> with the <see cref="UnrealTableAttribute.Name"/> as key.
        /// </summary>
        /// <remarks>
        /// Should be called in derived class as it adds the class instance to <see cref="FArchive.Tables"/>.
        /// </remarks>
        /// <param name="reader">Stream of binary data to read from.</param>
        public virtual void Deserialize(FArchive reader)
        {
            if (GetType().GetCustomAttributes(typeof(UnrealTableAttribute), false).First() is UnrealTableAttribute attr)
                reader.Tables.TryAdd(attr.Name, this);
        }

        private static void ThrowNotImplemented(string className)
            => throw new NotImplementedException($"UnrealTable deriving class {className} is not defining {nameof(UnrealTableAttribute)}, or isn't marked sealed.");

        /// <summary>
        /// Field to support deserialization by <see cref="FArchive.Read{T}(out List{T})"/> calls in deriving classes.
        /// </summary>
        protected List<T> _entries = new List<T>();
    }
}
