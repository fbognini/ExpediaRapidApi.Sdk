using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Properties
{
    public class Links
    {
        [JsonPropertyName("additional_rates")]
        public Link AdditionalRates { get; set; } = new();
        [JsonPropertyName("recommendations")]
        public Link Recommendations { get; set; } = new();
    }
}
