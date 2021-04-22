using Net.Bluewalk.DotNetUtils.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Net.Bluewalk.DotNetUtils
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class JsonLowerCaseUnderscoreIgnoreAttribute : Attribute
    {
    }

    public class JsonLowerCaseUnderscoreContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (!property.AttributeProvider.GetAttributes(typeof(JsonLowerCaseUnderscoreIgnoreAttribute), false).Any())
                property.PropertyName = property.PropertyName.ToUnderscoreSeparated();

            return property;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return base.CreateProperties(type, memberSerialization)
                .OrderBy(p => p.PropertyName)
                .ToList();
        }
    }
}
