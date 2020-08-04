using System;
using System.Collections.Generic;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Convert dictionary to specified object T
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ToObject<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            return (T)source.ToObject(typeof(T));
        }

        /// <summary>
        /// Convert dictionary to specified object type
        /// </summary>
        /// <param name="source"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ToObject(this IDictionary<string, object> source, Type type)
        {
            var someObject = Activator.CreateInstance(type);
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                someObjectType
                    .GetProperty(item.Key)
                    ?.SetValue(someObject, item.Value, null);
            }

            return someObject;
        }
    }
}
