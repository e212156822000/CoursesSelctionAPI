using System.Text.Json;

namespace CoursesSelectionUnitTest.Utils
{
    public static class HttpResponseMessageExtension
    {
        private static readonly JsonSerializerOptions s_jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static async Task<T?> ReadJsonResponseAsync<T>(this HttpResponseMessage response)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(jsonResponse, s_jsonOptions);
        }
    }
}
