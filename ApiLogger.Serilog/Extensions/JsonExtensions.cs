using Newtonsoft.Json;

namespace ApiLogger.Serilog
{
    internal static class JsonExtensions
    {
        public static string? FormatJson(this string json)
        {
            return IsValidJson(json) ? JsonConvert.DeserializeObject(json)?.ToString() : json;
        }

        public static bool IsValidJson(this string json)
        {
            return json != null && json.StartsWith("{") && json.EndsWith("}")
                || json != null && json.StartsWith("[") && json.EndsWith("]");
        }
    }
}
