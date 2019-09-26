using System;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Maps integer values to given constraints
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="oldFrom">The old from.</param>
        /// <param name="oldTo">The old to.</param>
        /// <param name="newFrom">The new from.</param>
        /// <param name="newTo">The new to.</param>
        /// <returns>System.Int32.</returns>
        public static int Map(this int value, int oldFrom, int oldTo, int newFrom, int newTo)
        {
            var scale = (double)(newTo - newFrom) / (oldTo - oldFrom);
            return (int)Math.Ceiling(newFrom + (value - oldFrom) * scale);
        }

        /// <summary>
        /// Clamps the specified minimum and maximum value of an integer
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>System.Int32.</returns>
        public static int Clamp(this int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
    }
}
