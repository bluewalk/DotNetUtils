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
    }
}
