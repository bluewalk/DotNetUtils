using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

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
        /// Get enum values
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<EnumValue> GetValues(Type t)
        {
            var values = Enum.GetValues(t);
            var result = new List<EnumValue>();

            foreach (var item in values)
            {
                var name = Enum.GetName(t, item);

                result.Add(new EnumValue()
                {
                    Name = name,
                    NamePretty = Regex.Replace(name, "(?<=.)([A-Z][a-z])", " $0"),
                    Value = item
                });
            }

            return result;
        }

        /// <summary>
        /// Get enum values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<EnumValue> GetValues<T>()
        {
            return GetValues(typeof(T));
        }

        /// <summary>
        /// Get enum information
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static EnumInfo GetInfo<T>()
        {
            return new(typeof(T));
        }

        /// <summary>
        /// Enum item
        /// </summary>
        public class EnumValue
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

        public class EnumInfo
        {
            public string Namespace { get; set; }
            public string Name { get; set; }
            public string FullName { get; set; }
            public bool IsFlag { get; set; }
            public List<EnumValue> Values { get; set; }

            public EnumInfo(Type type)
            {
                if (type?.IsEnum == false) throw new ArgumentException("Only Enums are supported");

                Namespace = type.Namespace;
                Name = type.Name;
                FullName = type.FullName;
                Values = GetValues(type);
                IsFlag = type.GetCustomAttribute<FlagsAttribute>() != null;
            }
        }
    }
}
