using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the inheritances.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>List&lt;Type&gt;.</returns>
        public static List<Type> GetInheritances(this Type value)
        {
            return value.Assembly.GetTypes()
                .Where(p => p.IsClass && p.IsSubclassOf(value))
                .ToList();
        }

        /// <summary>
        /// Gets the implementations.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>IEnumerable&lt;Type&gt;.</returns>
        public static IEnumerable<Type> GetImplementations(this Type value)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => value.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).ToList();
        }

        /// <summary>
        /// Gets the public properties.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>IEnumerable&lt;PropertyInfo&gt;.</returns>
        public static IEnumerable<PropertyInfo> GetPublicProperties(this Type type)
        {
            if (!type.IsInterface)
                return type.GetProperties();

            return (new[] {type})
                .Concat(type.GetInterfaces())
                .SelectMany(i => i.GetProperties());
        }

        /// <summary>
        /// Get the parent types
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetParentTypes(this Type type)
        {
            // is there any base type?
            if (type == null)
            {
                yield break;
            }

            // return all implemented or inherited interfaces
            foreach (var i in type.GetInterfaces())
            {
                yield return i;
            }

            // return all inherited types
            var currentBaseType = type.BaseType;
            while (currentBaseType != null)
            {
                yield return currentBaseType;
                currentBaseType = currentBaseType.BaseType;
            }
        }

        /// <summary>
        /// Checks if type implements given interface
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static bool Implements<TInterface>(this Type type) where TInterface : class
        {
            var interfaceType = typeof(TInterface);

            if (!interfaceType.IsInterface)
                throw new InvalidOperationException("Only interfaces can be implemented.");

            return (interfaceType.IsAssignableFrom(type));
        }
    }
}
