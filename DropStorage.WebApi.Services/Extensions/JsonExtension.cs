using System.Text.Json;

namespace DropStorage.WebApi.Services.Extensions
{
    public static class JsonExtension
    {
        public static string GetAsJsonClass<T>(this T obj) where T : class
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
