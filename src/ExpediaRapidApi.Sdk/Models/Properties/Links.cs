using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Properties
{
    public class Links
    {
        [JsonPropertyName("additional_rates")]
        public required LinkHref AdditionalRates { get; set; }
        [JsonPropertyName("recommendations")]
        public required LinkHref Recommendations { get; set; }
    }

    public class LinkHref
    {
        [JsonPropertyName("method")]
        public required string Method { get; set; }
        [JsonPropertyName("href")]
        public required string Href { get; set; }
        [JsonPropertyName("expires")]
        public string? Expires { get; set; }
    }
}
