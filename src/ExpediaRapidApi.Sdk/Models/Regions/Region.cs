using ExpediaRapidApi.Sdk.Models.Regions;
using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models
{
    public class Region
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("name_full")]
        public string? NameFull { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; } = string.Empty;

        [JsonPropertyName("coordinates")]
        public Coordinates? Coordinates { get; set; }

        [JsonPropertyName("associations")]
        public Associations? Associations { get; set; }

        [JsonPropertyName("ancestors")]
        public List<Ancestor>? Ancestors { get; set; }

        [JsonPropertyName("property_ids")]
        public List<string>? PropertyIds { get; set; }

        [JsonPropertyName("property_ids_expanded")]
        public List<string>? PropertyIdsExpanded { get; set; }

        [JsonPropertyName("descriptor")]
        public string? Descriptor { get; set; }

        [JsonPropertyName("descendants")]
        public Dictionary<string, List<string>>? Descendants { get; set; }
    }
}
