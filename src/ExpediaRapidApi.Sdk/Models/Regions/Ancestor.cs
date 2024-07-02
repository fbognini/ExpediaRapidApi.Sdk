using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Regions
{
    public class Ancestor
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
    }
}
