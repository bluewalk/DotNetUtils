using System;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Rounds the specified TimeSpan with rounding interval.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="roundingInterval">The rounding interval.</param>
        /// <param name="roundingType">Type of the rounding.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan Round(this TimeSpan time, TimeSpan roundingInterval, MidpointRounding roundingType)
        {
            return new(
                Convert.ToInt64(Math.Round(
                    time.Ticks / (decimal) roundingInterval.Ticks,
                    roundingType
                )) * roundingInterval.Ticks
            );
        }

        /// <summary>
        /// Rounds the specified TimeSpan with rounding interval.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="roundingInterval">The rounding interval.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan Round(this TimeSpan time, TimeSpan roundingInterval)
        {
            return Round(time, roundingInterval, MidpointRounding.ToEven);
        }

        /// <summary>
        /// Rounds the specified TimeSpan with rounding interval.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan Random(this TimeSpan time)
        {
            var seconds = time.TotalSeconds * StaticRandom.Instance.NextDouble();
            return TimeSpan.FromSeconds(seconds);
        }

        /// <summary>
        /// Strips the milliseconds.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan StripMilliseconds(this TimeSpan time)
        {
            return new(time.Days, time.Hours, time.Minutes, time.Seconds);
        }
    }
}
