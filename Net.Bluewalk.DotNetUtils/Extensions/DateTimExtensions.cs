using System;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class DateTimExtensions
    {
        /// <summary>
        /// Rounds the specified rounding interval.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="roundingInterval">The rounding interval.</param>
        /// <returns>DateTime.</returns>
        public static DateTime Round(this DateTime datetime, TimeSpan roundingInterval)
        {
            return new DateTime((datetime - DateTime.MinValue).Round(roundingInterval).Ticks);
        }

        /// <summary>
        /// Randoms the specified time span.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="timeSpan">The time span.</param>
        /// <returns>DateTime.</returns>
        public static DateTime Random(this DateTime value, TimeSpan timeSpan)
        {
            var seconds = timeSpan.TotalSeconds * StaticRandom.Instance.NextDouble();

            // Alternatively: return value.AddSeconds(-seconds);
            var span = TimeSpan.FromSeconds(seconds);
            return value - span;
        }
    }
}
