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

        /// <summary>
        /// Converts bytes into a readable string
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        public static string BytesToString(this long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            var bytes = Math.Abs(byteCount);
            var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            var num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num) + suf[place];
        }
    }
}
