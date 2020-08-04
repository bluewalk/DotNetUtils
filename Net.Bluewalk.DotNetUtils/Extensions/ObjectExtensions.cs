using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
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
        /// Convert object to dictionary
        /// </summary>
        /// <param name="source"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );
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

        /// <summary>
        /// Get value from object by specified property name
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValue<T>(this object obj, string property)
        {
            if (obj == null) return default;

            var prop = obj.GetType().GetProperty(property);
            if (prop == null) return default;

            return (T)prop.GetValue(obj, null);
        }

        #region Hashing

        /// <summary>
        /// Generate MD5 hash of object
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string GetMD5Hash(this object instance)
        {
            return instance.GetHash<MD5CryptoServiceProvider>();
        }

        /// <summary>
        /// Generate SHA1 hash of object
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string GetSHA1Hash(this object instance)
        {
            return instance.GetHash<SHA1CryptoServiceProvider>();
        }

        /// <summary>
        /// Generate has of privided algorithm for this object
        /// </summary>
        /// <typeparam name="T">HashAlgorithm</typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string GetHash<T>(this object instance) where T : HashAlgorithm, new()
        {
            var cryptoServiceProvider = new T();
            return ComputeHash(instance, cryptoServiceProvider);
        }

        /// <summary>
        /// Generates keyed hash, similar to GetHash only it uses a key to generate hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetKeyedHash<T>(this object instance, byte[] key) where T : KeyedHashAlgorithm, new()
        {
            var cryptoServiceProvider = new T { Key = key };
            return ComputeHash(instance, cryptoServiceProvider);
        }

        /// <summary>
        /// Private function to compute hash of provided cryptoServiceProvider
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="cryptoServiceProvider"></param>
        /// <returns></returns>
        private static string ComputeHash<T>(object instance, T cryptoServiceProvider) where T : HashAlgorithm, new()
        {
            var serializer = new DataContractSerializer(instance.GetType());
            using (var memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, instance);
                cryptoServiceProvider.ComputeHash(memoryStream.ToArray());

                return cryptoServiceProvider.Hash.Aggregate(string.Empty, (current, t) => current + t.ToString("X2"));
            }
        }
        #endregion
    }
}
