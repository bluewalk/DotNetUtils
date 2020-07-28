using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// To boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool ToBoolean(this string value)
        {
            return !string.IsNullOrEmpty(value) && Convert.ToBoolean(value);
        }

        // Parses boolean format strings, not optimized
        /// <summary>
        /// To boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="trueString">The true string.</param>
        /// <param name="falseString">The false string.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool ToBoolean(this string value, string trueString, string falseString)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            if (value.ToUpper().Equals(trueString.ToUpper()))
                return true;

            if (value.ToUpper().Equals(falseString.ToUpper()))
                return false;

            return false;
        }

        /// <summary>
        /// Parses array of string to array of int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32[].</returns>
        public static int[] ToIntArray(this string[] value)
        {
            int n;

            return value.Select(s => int.TryParse(s, out n) ? n : 0).ToArray();
        }

        /// <summary>
        /// Remove non ascii characters from string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string RemoveNonAscii(this string value)
        {
            return Regex.Replace(value, @"[^\x20-\x7E]", string.Empty);
        }

        /// <summary>
        /// Get X characters from the right
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="count">The count.</param>
        /// <returns>System.String.</returns>
        public static string Right(this string value, int count)
        {
            return count >= value.Length ? value : value.Substring(value.Length - count);
        }

        /// <summary>
        /// Get X characters from the left
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="count">The count.</param>
        /// <returns>System.String.</returns>
        public static string Left(this string value, int count)
        {
            return count >= value.Length ? value : value.Substring(0, count);
        }

        /// <summary>
        /// Convert string into guid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Guid.</returns>
        public static Guid ToGuid(this string value)
        {
            if (string.IsNullOrEmpty(value)) return Guid.Empty;

            try
            {
#if (NET35)
                return new Guid(value);
#else
                return Guid.Parse(value);
#endif
            }
            catch (FormatException)
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Returns matching Enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>T.</returns>
        public static T ToEnumValue<T>(this string value)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch
            {
                return (T)Enum.Parse(typeof(T), "Unspecified");
            }
        }

        /// <summary>
        /// Converts string to base64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string Base64Encode(this string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : Convert.ToBase64String(Encoding.ASCII.GetBytes(value));
        }

        /// <summary>
        /// Decodes a string from base64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string Base64Decode(this string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }

        /// <summary>
        /// Convert string to Double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Double.</returns>
        public static double ToDouble(this string value, double defaultValue)
        {
            if (string.IsNullOrEmpty(value)) return defaultValue;

            if (!double.TryParse(value.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                if (!double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                    result = defaultValue;

            return result;
        }

        /// <summary>
        /// Convert string to Double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Double.</returns>
        public static double ToDouble(this string value)
        {
            return ToDouble(value, 0);
        }

        /// <summary>
        /// Convert string to int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        public static int ToInt(this string value, int defaultValue)
        {
            if (string.IsNullOrEmpty(value)) return defaultValue;

            if (!int.TryParse(value, out var result))
                result = defaultValue;

            return result;
        }

        /// <summary>
        ///  Convert string to int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int ToInt(this string value)
        {
            return ToInt(value, 0);
        }

        /// <summary>
        /// String to NameValueCollection.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>NameValueCollection.</returns>
        public static NameValueCollection ToNameValueCollection(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            if (!value.IsValidJson()) return HttpUtility.ParseQueryString(value);

            var json = JObject.Parse(value);

            var result = new NameValueCollection();

            foreach (var item in json)
            {
                result.Add(item.Key, item.Value.ToString());
            }

            return result;
        }

        /// <summary>
        /// Determines whether string is valid JSON.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [is valid json] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsValidJson(this string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            value = value.Trim();
            if ((!value.StartsWith("{") || !value.EndsWith("}")) &&
                (!value.StartsWith("[") || !value.EndsWith("]"))) return false;

            try
            {
                JToken.Parse(value);
                return true;
            }
            catch (Exception) //some other exception
            {
                return false;
            }
        }

        /// <summary>
        /// Convert JSON string to object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>T.</returns>
        public static T DeserializeJson<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(
                value,
                new JsonSerializerSettings
                {
                    ContractResolver = new JsonLowerCaseUnderscoreContractResolver()
                }
            );
        }

        /// <summary>
        /// Convert Xml string to Object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>T.</returns>
        public static T DeserializeXml<T>(this string value)
        {
            var ser = new XmlSerializer(typeof(T));

            using (var sr = new StringReader(value))
                return (T)ser.Deserialize(sr);
        }

        /// <summary>
        /// Populates the object from json.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="target">The target.</param>
        public static void PopulateObjectFromJson(this string value, object target)
        {
            JsonConvert.PopulateObject(value, target, new JsonSerializerSettings
            {
                ContractResolver = new JsonLowerCaseUnderscoreContractResolver()
            });
        }

        /// <summary>
        /// Casts the specified JObject to given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>T.</returns>
        public static T Cast<T>(this JObject value)
        {
            return value.ToObject<T>(JsonSerializer.Create(new JsonSerializerSettings
            {
                ContractResolver = new JsonLowerCaseUnderscoreContractResolver()
            }));
        }

        /// <summary>
        /// Converts string from CamelCase to Underscore separated.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string ToUnderscoreSeparated(this string value)
        {
            return Regex.Replace(value, "(?<=.)([A-Z][a-z])", "_$0").ToLower();
        }

        /// <summary>
        /// Converts string to byte array
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] ToByteArray(this string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        /// <summary>
        /// Calculates the md5 hash.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string CalculateMD5Hash(this string value)
        {
            return value.ToByteArray().CalculateMD5Hash();
        }

        /// <summary>
        /// Returns the string value or default value if null
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="default">The default.</param>
        /// <returns>System.String.</returns>
        public static string OrDefault(this string str, string @default = default(string))
        {
            return string.IsNullOrEmpty(str) ? @default : str;
        }

        /// <summary>
        /// Check if a string is only digits
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsDigitsOnly(this string s)
        {
            var len = s.Length;
            for (var i = 0; i < len; ++i)
            {
                var c = s[i];
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Get only digits from a string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetDigits(this string s)
        {
            return Regex.Replace(s, @"[^\d]", string.Empty);
        }
    }
}
