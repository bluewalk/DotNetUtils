namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class BooleanExtensions
    {
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <param name="trueString">The true string.</param>
        /// <param name="falseString">The false string.</param>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public static string ToString(this bool value, string trueString, string falseString)
        {
            return value ? trueString : falseString;
        }
    }
}
