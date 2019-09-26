using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Formatting = Newtonsoft.Json.Formatting;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// To json.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="lowerCaseUnderscore">Use lower_case_underscore format</param>
        /// <returns>System.String.</returns>
        public static string ToJson(this object value, bool lowerCaseUnderscore = true)
        {
            return JsonConvert.SerializeObject(
                value,
                new JsonSerializerSettings
                {
                    ContractResolver = lowerCaseUnderscore ? new JsonLowerCaseUnderscoreContractResolver() : new DefaultContractResolver(),
                    DateTimeZoneHandling = DateTimeZoneHandling.Local,
                    Formatting = Formatting.None,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }

        /// <summary>
        /// To XML.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string ToXml(this object value)
        {
            string xml;

            using (var sw = new StringWriter())
            {
                using (var tw = new XmlTextWriter(sw))
                {
                    var ser = new XmlSerializer(value.GetType());
                    ser.Serialize(tw, value);
                }

                xml = sw.ToString();
            }

            return xml;
        }

        /// <summary>
        /// Returns the object value or default value if null
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="default">The default.</param>
        /// <returns>System.Object.</returns>
        public static object OrDefault(this string str, object @default)
        {
            return string.IsNullOrEmpty(str) ? @default : str;
        }
    }
}
