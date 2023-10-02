using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models
{
    public class ErrorField
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("value")]
        public object? Value { get; set; }
    }
}
