using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace UETools.TypeFactory
{
    /// <summary>
    /// Creates instances of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type with a parameterless constructor.</typeparam>
    public static class Factory<T> where T : new()
    {
        private static readonly Func<T> _createInstanceFunc = Expression.Lambda<Func<T>>(Expression.New(typeof(T)), true, Enumerable.Empty<ParameterExpression>()).Compile();

        /// <summary>
        /// Creates an instance of type <typeparamref name="T"/> by calling it's parameterless constructor.
        /// </summary>
        /// <returns>An instance of type <typeparamref name="T"/>.</returns>
        [return: NotNull]
#pragma warning disable CA1000 // Do not declare static members on generic types
        public static T CreateInstance() => _createInstanceFunc();

        public static Func<T> GetFactoryFunc() => _createInstanceFunc;
#pragma warning restore CA1000 // Do not declare static members on generic types
    }
}
