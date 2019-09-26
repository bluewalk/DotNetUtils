using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// To int8.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int ToInt8(this byte[] value)
        {
            return value[0];
        }

        /// <summary>
        /// To int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int ToInt16(this byte[] value)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(value);

            return BitConverter.ToInt16(value, 0);
        }

        /// <summary>
        /// To int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int ToInt32(this byte[] value)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(value);

            return BitConverter.ToInt32(value, 0);
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string ToStr(this byte[] value)
        {
            return Encoding.ASCII.GetString(value).Replace("\0", string.Empty).RemoveNonAscii();
        }

        /// <summary>
        /// Calculates the md5 hash.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string CalculateMD5Hash(this byte[] value)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(value);

            return hash.Aggregate(string.Empty, (current, t) => current + t.ToString("X2"));
        }
    }
}
