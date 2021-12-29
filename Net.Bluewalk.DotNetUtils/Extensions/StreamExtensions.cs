using System.IO;
using System.Threading.Tasks;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class StreamExtensions
    {
        /// <summary>
        /// To byte array.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] ToByteArray(this Stream value)
        {
            if (value == null) return null;

            var buffer = new byte[16 * 1024];
            using var ms = new MemoryStream();
            if (value.CanSeek)
                value.Seek(0, SeekOrigin.Begin);

            int read;
            while ((read = value.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            return ms.ToArray();
        }

        /// <summary>
        /// Reads to string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string ReadToString(this Stream value)
        {
            if (value == null) return null;

            using var reader = new StreamReader(value);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Reads to string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<string> ReadToStringAsync(this Stream value)
        {
            if (value == null)
                return null;
            using var streamReader = new StreamReader(value);
            return await streamReader.ReadToEndAsync();
        }
    }
}
