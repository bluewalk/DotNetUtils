using System;
using System.Linq;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class AttributeExtensions
    {
        /// <summary>
        /// Gets an attribute value.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the t attribute.</typeparam>
        /// <typeparam name="TValue">The type of the t value.</typeparam>
        /// <param name="type">The type.</param>
        /// <param name="valueSelector">The value selector.</param>
        /// <returns>TValue.</returns>
        public static TValue GetAttributeValue<TAttribute, TValue>(
            this Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return valueSelector(att);
            }
            return default(TValue);
        }
    }
}
