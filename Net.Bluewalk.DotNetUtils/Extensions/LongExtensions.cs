using System;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class LongExtensions
    {
        /// <summary>
        /// Creates DateTime from Unix Epoc time
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>DateTime.</returns>
        public static DateTime FromEpochToDateTime(this long value)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(value);
        }
    }
}
