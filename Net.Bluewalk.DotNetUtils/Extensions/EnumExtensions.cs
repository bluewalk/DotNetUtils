using System;
using System.Linq;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Determines whether [is one off] [the specified parameters].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if [is one off] [the specified parameters]; otherwise, <c>false</c>.</returns>
        public static bool IsOneOff(this Enum value, params Enum[] parameters)
        {
            return parameters.Contains(value);
        }
        
        /// <summary>
        /// Determines if enum flags overlap another enum flags
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool HasOverlappingFlags<T>(this T a, T b) where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();

            return values.Any(v => a.HasFlag(v) && b.HasFlag(v));
        }
        
        /// <summary>
        /// Get enum items
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<EnumItem> GetEnumItems(Type t)
        {
            var values = Enum.GetValues(t);
            var result = new List<EnumItem>();

            foreach (var item in values)
            {
                var name = Enum.GetName(t, item);

                result.Add(new EnumItem()
                {
                    Name = name,
                    NamePretty = Regex.Replace(name, "(?<=.)([A-Z][a-z])", " $0"),
                    Value = item
                });
            }

            return result;
        }

        /// <summary>
        /// Get enum items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<EnumItem> GetEnumItems<T>()
        {
            return GetEnumItems(typeof(T));
        }

        /// <summary>
        /// Enum item
        /// </summary>
        public class EnumItem
        {
            /// <summary>
            /// Name
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Pretty name (formatted)
            /// </summary>
            public string NamePretty { get; set; }

            /// <summary>
            /// Value
            /// </summary>
            public object Value { get; set; }
        }
    }
}
