using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Net.Bluewalk.DotNetUtils.Extensions
{
    public static class NameValueCollectionExtensions
    {
        public static string ToQueryString(this NameValueCollection value) => string.Join("&",
            value.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(value[a])));
    }
}
