using System;
using System.Collections.Generic;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class INumerableExtensions
    {
        /// <summary>
        /// Loops through every item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
                return;

            foreach (var item in source)
            {
                if (item != null)
                    action(item);
            }
        }
    }
}
