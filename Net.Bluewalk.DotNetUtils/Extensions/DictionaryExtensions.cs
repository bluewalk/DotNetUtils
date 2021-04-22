using System;
using System.Collections.Generic;
using System.Reflection;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Convert dictionary to specified object T
        /// </summary>
        /// <param name="source"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ToObject<T>(this IDictionary<string, object> source,
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public)
            where T : class, new()
        {
            return (T) source.ToObject(typeof(T), bindingFlags);
        }

        /// <summary>
        /// Convert dictionary to specified object type
        /// </summary>
        /// <param name="source"></param>
        /// <param name="type"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static object ToObject(this IDictionary<string, object> source, Type type,
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public)
        {
            var someObject = Activator.CreateInstance(type);
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                someObjectType
                    .GetProperty(item.Key, bindingFlags)
                    ?.SetValue(someObject, item.Value, null);
            }

            return someObject;
        }
    }
}
