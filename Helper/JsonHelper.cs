using System.Text.Json;

namespace Helper
{
    public static class JsonHelper
    {
        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        public static string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, Options);
        }
    }
}
