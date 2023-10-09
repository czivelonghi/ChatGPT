using System.Text;
using System.Text.Json;

namespace ChatGPT.Common
{
    public static class ObjectExtensions
    {
        public static string Serialize<T>(this T obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }

    public static class StringExtensions
    {
        public static T? Deserialize<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        public static StringContent ToJsonStringContent(this string json)
        {
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
