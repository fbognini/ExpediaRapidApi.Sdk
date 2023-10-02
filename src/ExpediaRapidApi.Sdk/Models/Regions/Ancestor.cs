using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Regions
{
    public class Ancestor
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("type")]
        public required string Type { get; set; }
    }
}
