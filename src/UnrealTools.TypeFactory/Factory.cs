using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace UnrealTools.TypeFactory
{
    /// <summary>
    /// Represents class for generating delegates instantiating passed type.
    /// </summary>
    public static class Factory
    {
        [DoesNotReturn]
        private static void ExceptionHelper(string message, Type type) => throw new TypeInitializationException(type.FullName, new Exception(message));
        /// <summary>
        /// Creates a delegate instantiating <paramref name="actualType"/>, making necessary checks to be sure it is of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The return type of compiled lambda.</typeparam>
        /// <param name="actualType"><see cref="Type"/> to instantiate.</param>
        /// <returns>Delegate instantiating <paramref name="actualType"/>.</returns>
        /// <remarks>Function makes necessary checks to ensure <paramref name="actualType"/> is actually instantiable</remarks>
        /// <exception cref="TypeInitializationException">Thrown if type is unsuitable for construction as <typeparamref name="T"/>. <see cref="Exception.InnerException"/> for message about error.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="actualType"/> is null</exception>
        public static Func<T> CreateInstanceFunction<T>(Type actualType)
        {
            var type = typeof(T);

            if (actualType is null)
                throw new ArgumentNullException(nameof(actualType));

            if (actualType.IsInterface)
                ExceptionHelper($"{actualType.FullName} is an interface.", actualType);

            if (actualType.IsAbstract)
                ExceptionHelper($"{actualType.FullName} is marked abstract.", actualType);

            if (actualType.IsGenericType && !actualType.IsConstructedGenericType)
                ExceptionHelper($"{actualType.FullName} is generic type, but not all generic type arguments were supplied.", actualType);

            if (!type.IsAssignableFrom(actualType))
                ExceptionHelper($"{actualType.FullName} is not assignable from {type.FullName}.", actualType);

            var constructorForParams = actualType.GetConstructor(Type.EmptyTypes);
            if (constructorForParams is null && !actualType.IsValueType)
                ExceptionHelper($"{actualType.FullName} can't be constructed.", actualType);

            if (type.IsInterface && actualType.IsValueType)
                return Expression.Lambda<Func<T>>(Expression.Convert(Expression.Default(actualType), type), true, Enumerable.Empty<ParameterExpression>()).Compile();
            else
                return Expression.Lambda<Func<T>>(Expression.New(constructorForParams), true, Enumerable.Empty<ParameterExpression>()).Compile();
        }
    }
}