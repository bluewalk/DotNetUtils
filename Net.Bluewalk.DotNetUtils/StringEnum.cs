﻿using System;
using System.Collections;
using System.Reflection;

namespace Net.Bluewalk.DotNetUtils
{
	#region Class StringEnum

	/// <summary>
	/// Helper class for working with 'extended' enums using <see cref="StringValueAttribute"/> attributes.
	/// </summary>
	public class StringEnum
	{
		#region Instance implementation

		private Type enumType;
		private static Hashtable stringValues = new Hashtable();

		/// <summary>
		/// Creates a new <see cref="StringEnum"/> instance.
		/// </summary>
		/// <param name="enumType">Enum type.</param>
		public StringEnum(Type enumType)
		{
			if (!enumType.IsEnum)
				throw new ArgumentException(string.Format("Supplied type must be an Enum.  Type was {0}", enumType.ToString()));

			this.enumType = enumType;
		}

		/// <summary>
		/// Gets the string value associated with the given enum value.
		/// </summary>
		/// <param name="valueName">Name of the enum value.</param>
		/// <returns>String Value</returns>
		public string GetStringValue(string valueName)
		{
			Enum enumType;
			string stringValue = null;
			try
			{
				enumType = (Enum) Enum.Parse(this.enumType, valueName);
				stringValue = GetStringValue(enumType);
			}
			catch (Exception) { }//Swallow!

			return stringValue;
		}

		/// <summary>
		/// Gets the string values associated with the enum.
		/// </summary>
		/// <returns>String value array</returns>
		public Array GetStringValues()
		{
			var values = new ArrayList();
			//Look for our string value associated with fields in this enum
			foreach (var fi in enumType.GetFields())
			{
				//Check for our custom attribute
				var attrs = fi.GetCustomAttributes(typeof (StringValueAttribute), false) as StringValueAttribute[];
				if (attrs.Length > 0)
					values.Add(attrs[0].Value);

			}

			return values.ToArray();
		}

		/// <summary>
		/// Gets the values as a 'bindable' list datasource.
		/// </summary>
		/// <returns>IList for data binding</returns>
		public IList GetListValues()
		{
			var underlyingType = Enum.GetUnderlyingType(enumType);
			var values = new ArrayList();
			//Look for our string value associated with fields in this enum
			foreach (var fi in enumType.GetFields())
			{
				//Check for our custom attribute
				var attrs = fi.GetCustomAttributes(typeof (StringValueAttribute), false) as StringValueAttribute[];
				if (attrs.Length > 0)
					values.Add(new DictionaryEntry(Convert.ChangeType(Enum.Parse(enumType, fi.Name), underlyingType), attrs[0].Value));

			}

			return values;

		}

		/// <summary>
		/// Return the existence of the given string value within the enum.
		/// </summary>
		/// <param name="stringValue">String value.</param>
		/// <returns>Existence of the string value</returns>
		public bool IsStringDefined(string stringValue)
		{
			return Parse(enumType, stringValue) != null;
		}

		/// <summary>
		/// Return the existence of the given string value within the enum.
		/// </summary>
		/// <param name="stringValue">String value.</param>
		/// <param name="ignoreCase">Denotes whether to conduct a case-insensitive match on the supplied string value</param>
		/// <returns>Existence of the string value</returns>
		public bool IsStringDefined(string stringValue, bool ignoreCase)
		{
			return Parse(enumType, stringValue, ignoreCase) != null;
		}

		/// <summary>
		/// Gets the underlying enum type for this instance.
		/// </summary>
		/// <value></value>
		public Type EnumType
		{
			get { return enumType; }
		}

		#endregion

		#region Static implementation

		/// <summary>
		/// Gets a string value for a particular enum value.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <returns>String Value associated via a <see cref="StringValueAttribute"/> attribute, or null if not found.</returns>
		public static string GetStringValue(Enum value)
		{
			string output = null;
			var type = value.GetType();

			if (stringValues.ContainsKey(value))
				output = (stringValues[value] as StringValueAttribute).Value;
			else 
			{
				//Look for our 'StringValueAttribute' in the field's custom attributes
				var fi = type.GetField(value.ToString());
				var attrs = fi.GetCustomAttributes(typeof (StringValueAttribute), false) as StringValueAttribute[];
				if (attrs.Length > 0)
				{
					stringValues.Add(value, attrs[0]);
					output = attrs[0].Value;
				}
					
			}
			return output;

		}

		/// <summary>
		/// Parses the supplied enum and string value to find an associated enum value (case sensitive).
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="stringValue">String value.</param>
		/// <returns>Enum value associated with the string value, or null if not found.</returns>
		public static object Parse(Type type, string stringValue)
		{
			return Parse(type, stringValue, false);
		}

		/// <summary>
		/// Parses the supplied enum and string value to find an associated enum value.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="stringValue">String value.</param>
		/// <param name="ignoreCase">Denotes whether to conduct a case-insensitive match on the supplied string value</param>
		/// <returns>Enum value associated with the string value, or null if not found.</returns>
		public static object Parse(Type type, string stringValue, bool ignoreCase)
		{
			object output = null;
            object def = null;
			string enumStringValue = null;

			if (!type.IsEnum)
				throw new ArgumentException(string.Format("Supplied type must be an Enum. Type was {0}", type.ToString()));

			//Look for our string value associated with fields in this enum
			foreach (var fi in type.GetFields())
			{
				//Check for our custom attribute
				var attrs = fi.GetCustomAttributes(typeof (StringValueAttribute), false) as StringValueAttribute[];
                if (attrs.Length > 0)
                {
                    enumStringValue = attrs[0].Value;

                    if (attrs[0].Default)
                        def = Enum.Parse(type, fi.Name);
                }

				//Check for equality then select actual enum value.
				if (string.Compare(enumStringValue, stringValue, ignoreCase) == 0)
				{
					output = Enum.Parse(type, fi.Name);
					break;
				}
			}

            if (output == null)
                output = def;

			return output;
		}

		/// <summary>
		/// Return the existence of the given string value within the enum.
		/// </summary>
		/// <param name="stringValue">String value.</param>
		/// <param name="enumType">Type of enum</param>
		/// <returns>Existence of the string value</returns>
		public static bool IsStringDefined(Type enumType, string stringValue)
		{
			return Parse(enumType, stringValue) != null;
		}

		/// <summary>
		/// Return the existence of the given string value within the enum.
		/// </summary>
		/// <param name="stringValue">String value.</param>
		/// <param name="enumType">Type of enum</param>
		/// <param name="ignoreCase">Denotes whether to conduct a case-insensitive match on the supplied string value</param>
		/// <returns>Existence of the string value</returns>
		public static bool IsStringDefined(Type enumType, string stringValue, bool ignoreCase)
		{
			return Parse(enumType, stringValue, ignoreCase) != null;
		}

		#endregion
	}

	#endregion

	#region Class StringValueAttribute

	/// <summary>
	/// Simple attribute class for storing String Values
	/// </summary>
	public class StringValueAttribute : Attribute
	{
		/// <summary>
		/// Creates a new <see cref="StringValueAttribute"/> instance.
		/// </summary>
		/// <param name="value">Value.</param>
		public StringValueAttribute(string value)
		{
			Value = value;
		}

        public StringValueAttribute() { }

        public string Value { get; set; }
        public bool Default { get; set; }
	}

	#endregion
}