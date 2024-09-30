using System.Text.Json;

namespace Service.Common.Extensions
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
        public static T Deserialize<T>(string obj)
        {
            return JsonSerializer.Deserialize<T>(obj, Options) ?? throw new JsonException($"Произошла ошибка с десериализацией:\n {obj}");
        }
    }
}
