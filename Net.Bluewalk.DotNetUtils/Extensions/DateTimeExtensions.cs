using System;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Rounds the specified rounding interval.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="roundingInterval">The rounding interval.</param>
        /// <returns>DateTime.</returns>
        public static DateTime Round(this DateTime datetime, TimeSpan roundingInterval)
        {
            return new((datetime - DateTime.MinValue).Round(roundingInterval).Ticks);
        }


        /// <summary>
        /// Rounds DateTime up to given timespan
        ///   var date = new DateTime(2010, 02, 05, 10, 35, 25, 450); // 2010/02/05 10:35:25
        //    var roundedUp = date.RoundUp(TimeSpan.FromMinutes(15)); // 2010/02/05 10:45:00
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime RoundUp(this DateTime dt, TimeSpan d)
        {
            var delta = d.Ticks - (dt.Ticks % d.Ticks);
            return new DateTime(dt.Ticks + delta, dt.Kind);
        }

        /// <summary>
        /// Round DateTime down to given timespan
        ///   var date = new DateTime(2010, 02, 05, 10, 35, 25, 450); // 2010/02/05 10:35:25
        ///   var roundedDown = date.RoundDown(TimeSpan.FromMinutes(15)); // 2010/02/05 10:30:00
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime RoundDown(this DateTime dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            return new DateTime(dt.Ticks - delta, dt.Kind);
        }

        /// <summary>
        /// Round DateTime up/down to nearest using timespan
        ///   var date = new DateTime(2010, 02, 05, 10, 35, 25, 450); // 2010/02/05 10:35:25
        ///   var roundedToNearest = date.RoundToNearest(TimeSpan.FromMinutes(15)); // 2010/02/05 10:30:00
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime RoundToNearest(this DateTime dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            var roundUp = delta > d.Ticks / 2;
            var offset = roundUp ? d.Ticks : 0;

            return new DateTime(dt.Ticks + offset - delta, dt.Kind);
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

        /// <summary>
        /// Get the closet date for target DayOfWeek
        /// </summary>
        /// <param name="date">The original date</param>
        /// <param name="target">The target of DayOfWeek</param>
        /// <returns>The closet date which is target day of week</returns>
        public static DateTime GetClosetDate(this DateTime date, DayOfWeek target)
        {
            if (date.DayOfWeek == target)
            {
                return date;
            }

            if (date.DayOfWeek > target)
            {
                return date.AddDays(target - date.DayOfWeek).Date;
            }
            else
            {
                return date.AddDays(target - date.DayOfWeek - 7).Date;
            }
        }

        /// <summary>
        /// Get the current month start date
        /// </summary>
        /// <param name="date">The original date</param>
        /// <returns>The current month start date</returns>
        public static DateTime GetMonthStartDate(this DateTime date)
        {
            return new(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Get the current month end date
        /// </summary>
        /// <param name="date">The original date</param>
        /// <returns>The current month start date</returns>
        public static DateTime GetMonthEndDate(this DateTime date)
        {
            var monthStateDate = date.GetMonthStartDate();
            return monthStateDate.AddMonths(1).AddDays(-1);
        }
    }
}
